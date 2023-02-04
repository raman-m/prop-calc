using RamanM.Properti.Calculator.Console;
using System.Reflection;

internal class Program
{
    static void Main(string[] args)
    {
        var console = new ConsoleService();
        var calculator = new ConsoleCalculator(console);

        var asm = Assembly.GetExecutingAssembly();
        var currentDir = Path.GetDirectoryName(asm.Location);

        while (true)
        {
            calculator.Welcome(asm);

            console.Write("Press Enter to make compilation test...");
            console.ReadLine();

            calculator.CompileFile(currentDir, "RoslynTest.csharp");

            bool quit = calculator.WaitUser();
            if (quit) break;
        }
    }
}
