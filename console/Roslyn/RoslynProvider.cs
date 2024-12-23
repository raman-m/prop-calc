using Basic.Reference.Assemblies;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace RamanM.Properti.Calculator.Console.Roslyn;

public class RoslynProvider : CodeDomProvider
{
    public ReferenceAssemblyKind TargetFramework { get; }

    [Obsolete]
    public override ICodeCompiler CreateCompiler()
        => new RoslynService(TargetFramework);

    [Obsolete]
    public override ICodeGenerator CreateGenerator()
        => new CSharpCodeProvider().CreateGenerator();
}
