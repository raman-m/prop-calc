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

            var choice = calculator.UserAction(userActions, 2);

            //console.Write("Press Enter to make compilation test...");
            //console.ReadLine();
            //calculator.CompileFile(currentDir, "RoslynTest.csharp");

            bool quit = calculator.WaitUser();
            if (quit) break;
        }
    }
    
    private static string[] userActions = new string[]
    {
        "   (1) Run basic compilation tests (no expressions)",
        "   (2) Run and compile examples of operations",
        "   (3) Add our own testing expressions, compile and run",
        "   (4) Skip and quit this session",
    };
}
