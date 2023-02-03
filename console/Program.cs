using System.Reflection;
using static System.Console;

namespace Properti.Calculator.Console;

internal class Program
{
    static void Main(string[] args)
    {
        var asm = Assembly.GetExecutingAssembly();
        var programName = asm.GetName().Name.Replace(".", " ");

        WriteLine();
        WriteLine($"Welcome to {programName} app!");
        ReadLine();
    }
}
