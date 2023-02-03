using RamanM.Properti.Calculator.Implementations;
using Roslyn.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using static System.Console;

internal class Program
{
    static void Main(string[] args)
    {
        Welcome();

        while (true)
        {
            Write("Press Enter to make compilation test...");
            ReadLine();

            CompileFile("RoslynTest.csharp");

            bool quit = WaitUser();
            if (quit) break;
        }
    }

    static void Welcome()
    {
        var asm = Assembly.GetExecutingAssembly();
        var programName = asm.GetName().Name.Replace(".", " ");

        WriteLine();
        ForegroundColor = ConsoleColor.Gray;
        WriteLine(asm.FullName);

        var anchor = typeof(Sum);
        ForegroundColor = ConsoleColor.Green;
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

    static bool WaitUser()
    {
        WriteLine();
        WriteLine("Enter Ctrl+Q to Quit, Ctrl+E to Exit, Ctrl+L to Clear the window");
        WriteLine("Or any key to start new expression test...");
        ConsoleKeyInfo info = ReadKey();
        if (info.Modifiers == ConsoleModifiers.Control)
        {
            if (info.Key == ConsoleKey.Q)
            {
                WriteLine("Soft quitting...");
                Environment.ExitCode = 0;
                return true; // while (true)
            }
            else if (info.Key == ConsoleKey.E)
            {
                WriteLine("Force exitting...");
                Environment.Exit(1);
            }
            else if (info.Key == ConsoleKey.L)
            {
                Clear();
                Welcome();
            }
        }
        return false;
    }

    static void CompileFile(string csharpFileName)
    {
        var csFile = Path.Combine(Environment.CurrentDirectory, csharpFileName);
        if (!File.Exists(csFile))
        {
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine("The C# code file doesn't exist! File: " + csFile);
            ForegroundColor = ConsoleColor.White;
            return;
        }
        var toFile = Path.Combine(Environment.CurrentDirectory, Path.GetFileNameWithoutExtension(csharpFileName) + ".dll");
        var csharp = File.ReadAllText(csFile);

        Compile(csharp, toFile);
    }

    static void Compile(string csharp, string toFile)
    {

        String[] referenceAssemblies = { }; //{ "System.dll", "System.Runtime.dll" };
        var options = new CompilerParameters(referenceAssemblies, toFile, true);
        options.GenerateExecutable = false;
        options.OutputAssembly = toFile;
        options.GenerateInMemory = false;

        var provider = new RoslynCodeDomProvider(TargetFramework.Net50);
        var results = provider.CompileAssemblyFromSource(options, new[] { csharp });
        //var results = provider.CompileAssemblyFromFile(options, new[] { csFile });

        WriteLine();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("Compiler return value: " + results.NativeCompilerReturnValue);
        WriteLine("The C# code compiled to: " + (results.PathToAssembly ?? toFile));

        if (results.Errors.Count == 0)
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine("No errors.");
        }
        else
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Compilation errors:");
            var errors = results.Errors.Cast<CompilerError>().ToArray();
            for (int i = 0; i < errors.Length; i++)
            {
                var e = errors[i];
                WriteLine($"#{i} : (line {e.Line}, column {e.Column}) : {e.ErrorText}");
            }
        }
        ForegroundColor = ConsoleColor.White;
    }
}
