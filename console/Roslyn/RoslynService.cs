using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RamanM.Properti.Calculator.Console.Roslyn
{
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
            var compilation = CSharpCompilation
                .Create(
                    Path.GetFileName(options.OutputAssembly),
                    syntaxTrees: sources.Select(x => CSharpSyntaxTree.ParseText(x)))
                .WithReferenceAssemblies(TargetFramework);

            var compilerResults = new CompilerResults(new TempFileCollection());
            AppendDiagnostics(compilation.GetDiagnostics());

            using var fileStream = new FileStream(options.OutputAssembly, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            var emitResult = compilation.Emit(fileStream);
            fileStream.Close();

            if (emitResult.Success)
            {
                compilerResults.NativeCompilerReturnValue = 0;
                if (options.GenerateInMemory)
                {
                    var bytes = File.ReadAllBytes(options.OutputAssembly);
                    compilerResults.CompiledAssembly = Assembly.Load(bytes);
                }
            }
            else
            {
                compilerResults.NativeCompilerReturnValue = 0;
                AppendDiagnostics(emitResult.Diagnostics);
            }

            return compilerResults;

            void AppendDiagnostics(IEnumerable<Diagnostic> diagnostics)
            {
                foreach (var diagnostic in diagnostics)
                {
                    var error = new CompilerError(
                        diagnostic.Location.SourceTree?.FilePath,
                        line: diagnostic.Location.GetLineSpan().StartLinePosition.Line,
                        column: diagnostic.Location.GetLineSpan().StartLinePosition.Character,
                        errorNumber: diagnostic.Id,
                        errorText: diagnostic.GetMessage());
                    compilerResults.Errors.Add(error);
                }
            }
        }
    }
}
