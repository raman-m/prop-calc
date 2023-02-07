using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using RamanM.Properti.Calculator.Console;
using RamanM.Properti.Calculator.Console.Interfaces;
using RamanM.Properti.Calculator.Interfaces;
using RamanM.Properti.Calculator.Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

internal class Program
{
    private static string[] userActions = new string[]
    {
        "   (1) Run basic compilation tests (no expressions)",
        "   (2) Run and compile examples of operations (fitness tests)",
        "   (3) Add our own testing expressions, compile and run",
        "   (4) Skip and quit this session",
    };

    static void ShowColors()
    {
        // Get an array with the values of ConsoleColor enumeration members.
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        // Save the current background and foreground colors.
        ConsoleColor currentBackground = Console.BackgroundColor;
        ConsoleColor currentForeground = Console.ForegroundColor;

        // Display all foreground colors except the one that matches the background.
        Console.WriteLine("All the foreground colors except {0}, the background color:",
                          currentBackground);
        foreach (var color in colors)
        {
            if (color == currentBackground) continue;

            Console.ForegroundColor = color;
            Console.WriteLine("   The foreground color is {0}.", color);
        }
        Console.WriteLine();
        // Restore the foreground color.
        Console.ForegroundColor = currentForeground;

        // Display each background color except the one that matches the current foreground color.
        Console.WriteLine("All the background colors except {0}, the foreground color:",
                          currentForeground);
        foreach (var color in colors)
        {
            if (color == currentForeground) continue;

            Console.BackgroundColor = color;
            Console.WriteLine("   The background color is {0}.", color);
        }

        // Restore the original console colors.
        Console.ResetColor();
        Console.WriteLine("\nOriginal colors restored...");
    }

    static void Main(string[] args)
    {
        //ShowColors();

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
                case 2:
                    RunFitnessTests(calculator, console, currentDir);
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

    internal static void RunFitnessTests(ConsoleCalculator calculator, IConsoleService console, string basePath)
    {
        console.WriteLine();
        console.Write("Loading fitness tests... ");
        var refAsm = typeof(SumTests).Assembly;

        var fitnessTypes = refAsm.GetTypes()
            .Where(t => t.Namespace?.Contains(".Fitness") ?? false)
            //.Select(t => t.Name)
            .ToArray();
        string[] actions = fitnessTypes
            .Select(t => t.Name)
            .Select(c => new { before = c.IndexOf("Fitness"), after = c.IndexOf("Fitness") + "Fitness".Length, name = c })
            .Select(a => new { method = a.name.Substring(0, a.before), suffix = a.name.Substring(a.before).ToLower().Insert(a.after - a.before, " ") })
            .Select(b => $"{ConsoleCalculator.PointerIndent}Perform {b.method} {b.suffix}")
            .ToArray();
        console.WriteLine("Done");

        var action = calculator.UserAction(actions, 0);
        var fitness = fitnessTypes[action];
        PerformFitnessTests(calculator, console, fitness, basePath);
    }

    internal static void PerformFitnessTests(ConsoleCalculator calculator, IConsoleService console, Type fitness, string basePath)
    {
        var tests = fitness.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        var names = tests.Select(t => t.Name).ToList();
        console.WriteLine();
        console.Color = ConsoleColor.White;
        console.Write("The ");
        console.Color = ConsoleColor.Yellow;
        console.Write(fitness.Name);
        console.Color = ConsoleColor.White;
        console.WriteLine(" class tests:");
        foreach (var name in names)
        {
            console.Color = ConsoleColor.White;
            console.Write($"{ConsoleCalculator.PointerIndent}({names.IndexOf(name) + 1}) ");
            console.Color = ConsoleColor.Blue;
            console.WriteLine(name);
        }
        console.Color = ConsoleColor.White;
        string[] actions = tests
            .Select(m => new
            {
                indent = ConsoleCalculator.PointerIndent,
                index = Array.IndexOf(tests, m),
                display = m.GetCustomAttribute<FactAttribute>().DisplayName
            })
            .Select(a => $"{a.indent}({a.index + 1}) {a.display}")
            .ToArray();
        var action = calculator.UserAction(actions, 0, "Select the test to perform:");
        var test = tests.Single(t => t.Name == names[action]);
        console.WriteLine();
        console.WriteLine($"Performing the test #{action + 1}...");
        var indent = "  ";

        console.Write($"{indent}Test name: ");
        console.Color = ConsoleColor.Blue;
        console.WriteLine(test.Name);
        console.ResetColor();

        var assertion = test.GetCustomAttribute<FactAttribute>().DisplayName;
        console.Write($"{indent}Assertion: ");
        console.Background = ConsoleColor.DarkGray; console.Write(assertion);
        console.ResetColor(); console.WriteLine(' ');

        var parts = assertion.Split("should return");
        var expected = parts[1].Trim(new char[] { ' ', '\'' });
        console.Write($"{indent}Expected value: ");
        console.Background = ConsoleColor.DarkGray; console.Write(expected);
        console.ResetColor(); console.WriteLine(' ');

        var operation = parts[0].Trim(); // C# code
        console.Write($"{indent}Operation C# : ");
        console.Background = ConsoleColor.DarkBlue; console.Write(operation);
        console.ResetColor(); console.WriteLine(' ');

        console.WriteLine();
        console.Write($"{indent}Running unit test... ");
        var instance = Activator.CreateInstance(fitness);
        var success = true;
        string status = null;
        try { test.Invoke(instance, new object[0]); }
        catch (Exception e) { success = false; status = e.Message; }
        PrintSuccess(console, success);

        string baseDir = Path.Combine(basePath, "Roslyn");
        console.WriteLine($"{indent}Compiling C# operation... ");

        var calcAsm = typeof(IBinaryOperation).Assembly;
        var assemblies = new List<Assembly>();
        AddReferencedAssemblies(calcAsm, assemblies);

        string[] references = assemblies.Select(a => a.Location).Distinct().ToArray();
        var csFile = Path.Combine(baseDir, "OperationSample.csharp");
        string csharpFormat = File.ReadAllText(csFile);
        string csharp = csharpFormat.Replace("{0}", operation);
        var toFile = Path.Combine(baseDir, "OperationSample.dll");
        string path = calculator.Compile(csharp, toFile, references, indent + indent);
    }

    private static void AddReferencedAssemblies(Assembly master, List<Assembly> list)
    {
        list.Add(master);
        var refs = master.GetReferencedAssemblies();
        foreach (AssemblyName @ref in refs)
        {
            var assmbl = Assembly.Load(@ref.FullName);
            AddReferencedAssemblies(assmbl, list);
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

        var refAsm = Assembly.LoadFrom(Path.Combine(baseDir, "Test1.dll"));
        var systemRefs = refAsm.GetReferencedAssemblies();
        var assemblies = new List<Assembly>() { refAsm };
        foreach (AssemblyName asm in systemRefs)
            assemblies.Add(Assembly.Load(asm.FullName));
        references = assemblies.Select(a => a.Location).ToArray();
        //references = new string[] { Path.Combine(baseDir, "Test1.dll") };
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
        PrintSuccess(console, success, status, indent);
        console.Write($"{testName}: ");
        PrintSuccess(console, success);
    }

    private static void PrintSuccess(IConsoleService console, bool success, string status = null, string indent = null, bool? newLine = null)
    {
        var statusName = status ?? (success ? "Success" : "Failed");
        var prefix = indent ?? string.Empty;
        var nline = newLine ?? true;
        console.Color = success ? ConsoleColor.Green : ConsoleColor.Red;
        console.Write(prefix + statusName);
        console.Color = ConsoleColor.White;
        if (nline)
            console.WriteLine();
    }
}
