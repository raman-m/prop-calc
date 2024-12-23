using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace RamanM.Properti.Calculator.Console.Roslyn;

public class RoslynService : ICodeCompiler
{
    public ReferenceAssemblyKind TargetFramework { get; }

    public RoslynService(ReferenceAssemblyKind target)
    {
        TargetFramework = target;
    }

    public CompilerResults CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit compilationUnit)
    {
        throw new NotImplementedException();
    }

    public CompilerResults CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] compilationUnits)
    {
        throw new NotImplementedException();
    }

    public CompilerResults CompileAssemblyFromFile(CompilerParameters options, string fileName)
    {
        throw new NotImplementedException();
    }

    public CompilerResults CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
    {
        throw new NotImplementedException();
    }

    public CompilerResults CompileAssemblyFromSource(CompilerParameters options, string source)
    {
        return CompileAssemblyFromSourceBatch(options, new[] { source });
    }

    public CompilerResults CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
    {
        return CompileAssembly(options, sources);
    }

    public CompilerResults CompileAssembly(CompilerParameters options, string source, string[]? references = null)
    {
        return CompileAssembly(options, new[] { source }, references);
    }

    public CompilerResults CompileAssembly(CompilerParameters options, string[] sources, string[]? references = null)
    {
        var defaultRefs = new[] {
            typeof(object).Assembly.Location,
        };
        var refs = references ?? defaultRefs;
        foreach (string dllPath in refs)
        {
            if (!options.ReferencedAssemblies.Contains(dllPath))
                options.ReferencedAssemblies.Add(dllPath);
        }
        var metaReferences = options.ReferencedAssemblies
            .Cast<string>()
            .Select(asm => MetadataReference.CreateFromFile(asm))
            .ToList();
        var compilation = CSharpCompilation.Create(
            Path.GetFileName(options.OutputAssembly),
            syntaxTrees: sources.Select(x => CSharpSyntaxTree.ParseText(x)),
            references: metaReferences,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        _ = compilation.WithReferenceAssemblies(TargetFramework);

        return Compile(compilation, options);
    }

    private void AppendDiagnostics(IEnumerable<Diagnostic> diagnostics, CompilerResults results)
    {
        foreach (var diagnostic in diagnostics)
        {
            var message = diagnostic.GetMessage();
            var exists = results.Errors.Cast<CompilerError>().Any(e => e.ErrorText == message);
            if (!exists)
            {
                var error = new CompilerError(
                    diagnostic.Location.SourceTree?.FilePath,
                    line: diagnostic.Location.GetLineSpan().StartLinePosition.Line,
                    column: diagnostic.Location.GetLineSpan().StartLinePosition.Character,
                    errorNumber: diagnostic.Id,
                    errorText: diagnostic.GetMessage());
                error.IsWarning = diagnostic.Severity != DiagnosticSeverity.Error;
                results.Errors.Add(error);
            }
        }
    }

    protected CompilerResults Compile(CSharpCompilation compilation, CompilerParameters options)
    {
        var compilerResults = new CompilerResults(new TempFileCollection());
        AppendDiagnostics(compilation.GetDiagnostics(), compilerResults);

        using var fileStream = new FileStream(options.OutputAssembly, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        var emitResult = compilation.Emit(fileStream);
        fileStream.Close();

        if (emitResult.Success)
        {
            compilerResults.NativeCompilerReturnValue = 1;
            if (options.GenerateInMemory)
            {
                var bytes = File.ReadAllBytes(options.OutputAssembly);
                compilerResults.CompiledAssembly = Assembly.Load(bytes);
            }
        }
        else
        {
            compilerResults.NativeCompilerReturnValue = -1;
            AppendDiagnostics(emitResult.Diagnostics, compilerResults);
        }

        return compilerResults;
    }
}
