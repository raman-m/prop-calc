using RamanM.Properti.Calculator.Console;
using RamanM.Properti.Calculator.Console.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

internal class Program
{
    private static string[] userActions = new string[]
    {
        "   (1) Run basic compilation tests (no expressions)",
        "   (2) Run and compile examples of operations",
        "   (3) Add our own testing expressions, compile and run",
        "   (4) Skip and quit this session",
    };

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
            switch (++choice)
            {
                case 1:
                    RunCompilationTests(calculator, console, currentDir);
                    break;
                case 4:
                    console.WriteLine("Skipped");
                    break;
                default:
                    console.WriteLine($"Sorry, no action with code {choice} to perform!");
                    break;
            }

            bool quit = calculator.WaitUser();
            if (quit) break;
        }
    }

    internal static void RunCompilationTests(ConsoleCalculator calculator, IConsoleService console, string currentDir)
    {
        string baseDir = Path.Combine(currentDir, "Roslyn");
        console.WriteLine("Going to run basic compilation tests...");
        string indent = "  ";

        var typeName = "Test1";
        var testName = StartTest(console, typeName, "[{0}]: No references");
        string[] references = Array.Empty<string>();
        var path = calculator.CompileFile(baseDir, typeName + ".csharp", references, indent);
        PrintTest(console, typeName, testName, path, indent);

        typeName = "Test2";
        testName = StartTest(console, typeName, "[{0}]: With references");
        references = new string[] { Path.Combine(baseDir, "Test1.dll") };
        path = calculator.CompileFile(baseDir, typeName + ".csharp", references, indent);
        PrintTest(console, typeName, testName, path, indent);
    }

    private static string StartTest(IConsoleService console, string typeName, string format)
    {
        var testName = typeName.Insert(typeName.IndexOf(typeName.First(char.IsDigit)), " ");
        console.WriteLine();
        console.WriteLine(string.Format(format, testName));
        return testName;
    }

    private static void PrintTest(IConsoleService console, string typeName, string testName, string path, string indent)
    {
        bool success = false;
        string status = string.Empty;
        if (!string.IsNullOrEmpty(path))
        {
            console.Write($"{indent}Reflecting of the {typeName} type... ");
            var dll = Assembly.LoadFrom(path);
            var types = dll.GetTypes();
            console.WriteLine("Defined types: " + string.Join(", ", types.Select(t => t.Name).Where(s => !s.Contains("Attribute"))));
            var @namespace = "RoslynTests";
            try
            {
                var fullName = $"{@namespace}.{typeName}";
                var instance = dll.CreateInstance(fullName);
                if (instance == null)
                    throw new Exception("No instance");

                Type t = dll.GetType(fullName, true);
                var len = t.GetMethod("Length").Invoke(instance, new object[] { "123" });
                status = $"Type '{typeName}' loaded successfully.";
                success = true;
            }
            catch (Exception e)
            {
                status = e.Message;
                success = false;
            }
        }
        else
        {
            status = "Compilation failed!";
            success = false;
        }
        console.Color = success ? ConsoleColor.Green : ConsoleColor.Red;
        console.WriteLine(indent + status);
        console.Color = ConsoleColor.White;
        var result = success ? "Success" : "Failed";
        console.Write($"{testName}: ");
        console.Color = success ? ConsoleColor.Green : ConsoleColor.Red;
        console.WriteLine(result);
        console.Color = ConsoleColor.White;
    }
}
