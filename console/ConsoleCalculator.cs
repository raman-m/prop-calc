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
        console.WriteLine("Enter Ctrl+Q to Quit, Ctrl+E to Exit, Ctrl+L to Clear the log");
        console.Write("Or press any key to restart... ");
        ConsoleKeyInfo info = console.ReadKey(true);
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
                console.WriteLine();
                console.Clear();
            }
        }
        console.WriteLine();
        return false;
    }

    protected void EnsureScrolling(ref int top)
    {
        if (console.CursorTop >= console.BufferHeight - 1)
            top--;
    }

    protected void PrintAction(string action, ConsoleColor color, int line)
    {
        console.Color = color;
        console.SetCursor(0, line);
        console.Write(action);
    }

    protected const string Pointer = "-> ";

    protected void PointToAction(string[] actions, int to, ConsoleColor color, int line)
    {
        var pointTo = actions[to];
        pointTo = pointTo.Remove(0, Pointer.Length).Insert(0, Pointer);
        PrintAction(pointTo, color, line);
    }

    public int UserAction(string[] actions, int selected)
    {
        console.CursorVisible = false;
        console.WriteLine();
        console.WriteLine("Your next action?");
        var start = console.GetCursor();
        foreach (var action in actions)
        {
            EnsureScrolling(ref start.Top); // scroll start position with window log
            console.WriteLine(action);
        }
        console.Color = ConsoleColor.DarkGray;
        console.Write("Use arrows [Up, Down] to make the choice, and press Enter... ");
        var end = console.GetCursor();

        // Print selected action
        PointToAction(actions, selected, ConsoleColor.Yellow, start.Top + selected);
        console.SetCursor(end.Left, end.Top);

        int index = selected;
        while (true)
        {
            ConsoleKeyInfo info = console.ReadKey(true);
            console.Color = ConsoleColor.White;
            console.SetCursor(end.Left, end.Top);
            int old = index;
            if (info.Key == ConsoleKey.UpArrow)
            {
                console.Write('A');
                index--;
                if (index < 0)
                    index = 0;
            }
            else if (info.Key == ConsoleKey.DownArrow)
            {
                console.Write('V');
                index++;
                if (index >= actions.Length)
                    index = actions.Length - 1;
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                console.Write(index + 1);
                break; ;
            }
            else
            {
                console.Write(' ');
                continue;
            }
            PrintAction(actions[old], ConsoleColor.White, start.Top + old);
            // Re-print new action as selected
            PointToAction(actions, index, ConsoleColor.Yellow, start.Top + index);

            console.Beep();
            console.SetCursor(end.Left, end.Top);
            console.Write(' ');
            console.SetCursor(end.Left, end.Top);
        }
        console.SetCursor(end.Left, end.Top);
        console.WriteLine();
        console.Beep((index + 1) * 1000, 300); // 1 up to 4 KHz during 0.3 seconds
        console.CursorVisible = true;
        return index;
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
