﻿using Basic.Reference.Assemblies;
using RamanM.Properti.Calculator.Console.Interfaces;
using RamanM.Properti.Calculator.Console.Roslyn;
using RamanM.Properti.Calculator.Implementations;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;

namespace RamanM.Properti.Calculator.Console;

internal class ConsoleCalculator(IConsoleService console) : Calculator(console)
{
    private readonly IConsoleService console = console;

    public void Welcome(Assembly app)
    {
#if NET9_0_OR_GREATER
        var version = ".NET 9";
#else
        var version = ".NET < 9";
#endif
        console.Color = ConsoleColor.Gray;
        console.WriteLine(version);

        var programName = app?.GetName()?.Name?.Replace(".", " ") ?? "?";
        console.WriteLine(app?.FullName ?? "?");

        var anchor = typeof(Sum);
        console.Color = ConsoleColor.Green;
        console.WriteLine($"Loaded: {anchor.Assembly.FullName}");

        console.WriteLine();
        console.Color = ConsoleColor.Yellow;
        console.WriteLine($"Welcome to {programName} app!");

        string space = anchor.Namespace!;
        var operations = from t in anchor.Assembly.GetTypes()
                         where t.IsClass && t.Namespace == space
                             && !t.Name.Contains("Operation") && !t.Name.Contains("Constant") && !t.Name.StartsWith("<>c__")
                         select t;

        console.WriteLine();
        console.Color = ConsoleColor.White;
        console.Write("Defined operations: ");
        console.Color = ConsoleColor.Blue;
        operations.ToList().ForEach(t => console.Write(t.Name + ", "));

        var (Left, Top) = console.GetCursor();
        console.SetCursor(Left - 2, Top);
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
                console.WriteLine("Quitting...");
                Environment.ExitCode = 0;
                return true;
            }
            else if (info.Key == ConsoleKey.E)
            {
                console.WriteLine("Exitting...");
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

    public bool AskYesNo(string? asking = null)
    {
        var ask = asking ?? "Continue?";
        console.Write($"{ask} (Y/N)  ");
        console.CursorLeft--; // clear previous answer
        while (true)
        {
            ConsoleKeyInfo info = console.ReadKey(true);
            if (info.Key == ConsoleKey.Y)
            {
                console.Write(nameof(ConsoleKey.Y));
                return true;
            }
            else if (info.Key == ConsoleKey.N)
            {
                console.Write(nameof(ConsoleKey.N));
                return false;
            }
            else
            {
                continue;
            }
        }
    }

    protected void EnsureScrolling(ref int top)
    {
        if (console.CursorTop >= console.BufferHeight - 1)
            top--;
    }

    protected void PrintAction(string action, ConsoleColor color)
    {
        var max = console.BufferWidth;
        console.Color = color;
        console.Write(action.Length <= max ? action : action[..max]);
    }

    protected void PrintAction(string action, ConsoleColor color, int line)
    {
        console.SetCursor(0, line);
        PrintAction(action, color);
    }

    public const string Pointer         = "-> ";
    public const string PointerIndent   = "   ";

    protected void PointToAction(string[] actions, int to, ConsoleColor color, int line)
    {
        var pointTo = actions[to];
        pointTo = pointTo.Remove(0, Pointer.Length).Insert(0, Pointer);
        PrintAction(pointTo, color, line);
    }

    public int UserAction(string[] actions, int selected, string? header = null)
    {
        console.CursorVisible = false;
        console.WriteLine();
        console.WriteLine(header ?? "Your next action?");
        var (_, Top) = console.GetCursor();
        foreach (var action in actions)
        {
            EnsureScrolling(ref Top); // scroll start position with window log
            PrintAction(action, ConsoleColor.White);
            console.WriteLine();
        }
        console.Color = ConsoleColor.DarkGray;
        console.Write("Use arrows [Up, Down] to make the choice, and press Enter... ");
        var end = console.GetCursor();

        // Print selected action
        PointToAction(actions, selected, ConsoleColor.Yellow, Top + selected);
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
            PrintAction(actions[old], ConsoleColor.White, Top + old);
            // Re-print new action as selected
            PointToAction(actions, index, ConsoleColor.Yellow, Top + index);

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

    public string CompileFile(string appPath, string csharpFileName, string[] refs, string indent = "")
    {
        var csFile = Path.Combine(appPath, csharpFileName);
        if (!File.Exists(csFile))
        {
            console.Color = ConsoleColor.DarkRed;
            console.WriteLine($"{indent}The C# code file doesn't exist! File: {csFile}");
            console.Color = ConsoleColor.White;
            return string.Empty;
        }
        var toFile = Path.Combine(appPath, Path.GetFileNameWithoutExtension(csharpFileName) + ".dll");
        var csharp = File.ReadAllText(csFile);

        return Compile(csharp, toFile, refs, indent);
    }

    public string Compile(string csharp, string toFile, string[]? references = null, string indent = "")
    {

        string[] referenceAssemblies = references ?? [];
        var options = new CompilerParameters(referenceAssemblies, toFile, true)
        {
            GenerateExecutable = false,
            OutputAssembly = toFile,
            GenerateInMemory = false
        };

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var roslyn = new RoslynService(ReferenceAssemblyKind.Net80);
        CompilerResults results;
        if (references == null || references.Length == 0)
            results = roslyn.CompileAssemblyFromSource(options, csharp);
        else
            results = roslyn.CompileAssembly(options, csharp, references);

        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = string.Format("{0}.{1:000} second(s)", ts.Seconds, ts.Milliseconds);

        console.Color = ConsoleColor.Yellow;
        console.WriteLine(indent + "Compiler return value: " + results.NativeCompilerReturnValue);
        console.WriteLine(indent + "Compilation took: " + elapsedTime);

        if (results.Errors.Count == 0)
        {
            console.WriteLine(indent + "Compiled to: " + (results.PathToAssembly ?? toFile));
            console.Color = ConsoleColor.Green;
            console.WriteLine(indent + "Successful");
            console.Color = ConsoleColor.White;
            return toFile;
        }

        console.Color = ConsoleColor.Red;
        console.WriteLine(indent + "Compilation errors:");
        var errors = results.Errors.Cast<CompilerError>()
            .OrderBy(e => e.Line).ThenBy(e => e.Column).ToArray();
        for (int i = 0; i < errors.Length; i++)
        {
            var e = errors[i];
            var type = e.IsWarning ? "warning" : "error";
            console.Color = e.IsWarning ? ConsoleColor.Yellow : ConsoleColor.Red;
            console.WriteLine($"{indent}#{i} (line {e.Line}, column {e.Column}) {type}: {e.ErrorText}");
        }
        console.Color = ConsoleColor.White;
        return string.Empty;
    }
}
