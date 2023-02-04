using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RamanM.Properti.Calculator.Console.Interfaces;
using RamanM.Properti.Calculator.Implementations;
using RamanM.Properti.Calculator.Interfaces;
using Roslyn.CodeDom;

namespace RamanM.Properti.Calculator.Console;

internal class ConsoleCalculator : Calculator
{
    private readonly IConsoleService console;

    public ConsoleCalculator(IConsoleService console)
        : base(console)
    {
        this.console = console;
    }

    public void Welcome(Assembly app)
    {
        var programName = app.GetName().Name.Replace(".", " ");

        console.WriteLine();
        console.Color = ConsoleColor.Gray;
        console.WriteLine(app.FullName);

        var anchor = typeof(Sum);
        console.Color = ConsoleColor.Green;
        console.WriteLine($"Loaded: {anchor.Assembly.FullName}");

        console.WriteLine();
        console.Color = ConsoleColor.Yellow;
        console.WriteLine($"Welcome to {programName} app!");

        string space = anchor.Namespace;
        var operations = from t in anchor.Assembly.GetTypes()
                where t.IsClass && t.Namespace == space
                    && !t.Name.Contains("Operation") && !t.Name.Contains("Constant")
                select t;

        console.WriteLine();
        console.Color = ConsoleColor.White;
        console.Write("Defined operations: ");
        console.Color = ConsoleColor.Green;
        operations.ToList().ForEach(t => console.Write(t.Name + ", "));

        var cursor = console.GetCursor();
        console.SetCursor(cursor.Left - 2, cursor.Top);
        console.WriteLine(' ');

        console.Color = ConsoleColor.White;
    }

    public bool WaitUser()
    {
        console.WriteLine();
        console.WriteLine("Enter Ctrl+Q to Quit, Ctrl+E to Exit, Ctrl+L to Clear the window");
        console.WriteLine("Or any key to start new expression test...");
        ConsoleKeyInfo info = console.ReadKey();
        if (info.Modifiers == ConsoleModifiers.Control)
        {
            if (info.Key == ConsoleKey.Q)
            {
                console.WriteLine("Soft quitting...");
                Environment.ExitCode = 0;
                return true;
            }
            else if (info.Key == ConsoleKey.E)
            {
                console.WriteLine("Force exitting...");
                Environment.Exit(1);
            }
            else if (info.Key == ConsoleKey.L)
            {
                console.Clear();
            }
        }
        return false;
    }

    public void CompileFile(string appPath, string csharpFileName)
    {
        var csFile = Path.Combine(appPath, csharpFileName);
        if (!File.Exists(csFile))
        {
            console.Color = ConsoleColor.DarkRed;
            console.WriteLine("The C# code file doesn't exist! File: " + csFile);
            console.Color = ConsoleColor.White;
            return;
        }
        var toFile = Path.Combine(appPath, Path.GetFileNameWithoutExtension(csharpFileName) + ".dll");
        var csharp = File.ReadAllText(csFile);

        Compile(csharp, toFile);
    }

    public void Compile(string csharp, string toFile)
    {

        string[] referenceAssemblies = { }; //{ "System.dll", "System.Runtime.dll" };
        var options = new CompilerParameters(referenceAssemblies, toFile, true);
        options.GenerateExecutable = false;
        options.OutputAssembly = toFile;
        options.GenerateInMemory = false;

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var provider = new RoslynCodeDomProvider(TargetFramework.Net50);
        var results = provider.CompileAssemblyFromSource(options, new[] { csharp });
        //var results = provider.CompileAssemblyFromFile(options, new[] { csFile });
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = string.Format("{0}.{1:000} second(s)", ts.Seconds, ts.Milliseconds);

        console.WriteLine();
        console.Color = ConsoleColor.Yellow;
        console.WriteLine("Compiler return value: " + results.NativeCompilerReturnValue);
        console.WriteLine("Compilation took: " + elapsedTime);
        console.WriteLine("The C# code compiled to: " + (results.PathToAssembly ?? toFile));

        if (results.Errors.Count == 0)
        {
            console.Color = ConsoleColor.Green;
            console.WriteLine("No errors.");
        }
        else
        {
            console.Color = ConsoleColor.Red;
            console.WriteLine("Compilation errors:");
            var errors = results.Errors.Cast<CompilerError>().ToArray();
            for (int i = 0; i < errors.Length; i++)
            {
                var e = errors[i];
                console.WriteLine($"#{i} : (line {e.Line}, column {e.Column}) : {e.ErrorText}");
            }
        }
        console.Color = ConsoleColor.White;
    }
}
