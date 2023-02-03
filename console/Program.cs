using RamanM.Properti.Calculator.Implementations;
using System.Reflection;
using static System.Console;

internal class Program
{
    static void Main(string[] args)
    {
        Welcome();
        ReadLine();
    }

    static void Welcome()
    {
        var asm = Assembly.GetExecutingAssembly();
        var programName = asm.GetName().Name.Replace(".", " ");

        WriteLine();
        ForegroundColor = ConsoleColor.Green;
        WriteLine(asm.FullName);

        var anchor = typeof(Sum);
        WriteLine($"Loaded: {anchor.Assembly.FullName}");

        WriteLine();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine($"Welcome to {programName} app!");

        string space = anchor.Namespace;
        var q = from t in anchor.Assembly.GetTypes()
                where t.IsClass && t.Namespace == space
                    && !t.Name.Contains("Operation") && !t.Name.Contains("Constant")
                select t;

        WriteLine();
        ForegroundColor = ConsoleColor.White;
        Write("Defined operations: ");
        ForegroundColor = ConsoleColor.Green;
        q.ToList().ForEach(t => Write(t.Name + ", "));

        var cursor = GetCursorPosition();
        SetCursorPosition(cursor.Left - 2, cursor.Top);
        WriteLine(' ');

        ForegroundColor = ConsoleColor.White;
    }
}
