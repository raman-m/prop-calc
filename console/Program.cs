using RamanM.Properti.Calculator.Console;
using RamanM.Properti.Calculator.Console.Interfaces;
using RamanM.Properti.Calculator.Interfaces;
using RamanM.Properti.Calculator.Tests;
using System.Reflection;
using Xunit;

var console = new ConsoleService();
var calculator = new ConsoleCalculator(console);

var asm = Assembly.GetExecutingAssembly();
var currentDir = Path.GetDirectoryName(asm.Location) ?? ".";

while (true)
{
    calculator.Welcome(asm);

    var choice = calculator.UserAction(App.UserActions, 2);
    switch (++choice)
    {
        case 1:
            App.RunCompilationTests(calculator, console, currentDir);
            break;
        case 2:
            App.RunFitnessTests(calculator, console, currentDir);
            break;
        case 3:
            App.RunCustomTests(calculator, console, currentDir);
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

internal class App
{
    public static string[] UserActions = new string[]
    {
        "   (1) Run basic compilation tests (no expressions)",
        "   (2) Run and compile examples of operations (fitness tests)",
        "   (3) Add your testing expressions, compile and evaluate",
        "   (4) Skip and quit this session",
    };

    internal static void RunCustomTests(ConsoleCalculator calculator, IConsoleService console, string basePath)
    {
        while (true)
        {
            var expression = AskExpression(calculator, console);
            var result = EvaluateExpression(calculator, console, expression, basePath);
            console.ResetColor();
            console.Write("Result: ");
            PrintLineOnBackground(console, result, ConsoleColor.DarkBlue);
            console.WriteLine();
            var yes = calculator.AskYesNo("Going to test new expression?");
            if (!yes)
                break;
        }
    }

    internal static string AskExpression(ConsoleCalculator calculator, IConsoleService console)
    {
        console.WriteLine();
        console.WriteLine("Enter your C# expression with defined operations:");
        var start = console.GetCursor();
        console.Color = ConsoleColor.DarkBlue;
        console.Write("> ");
        console.Color = ConsoleColor.Blue;
        EnsureScrolling(console, ref start.Top);
        var expression = console.ReadLine();
        console.Color = ConsoleColor.White;
        while (!calculator.AskYesNo("Is it final and correct?"))
        {
            console.SetCursor(start.Left, start.Top);
            EnsureScrolling(console, ref start.Top);
            expression = ReadLine(console, expression);
            console.ResetColor();
        }
        return expression;
    }

    internal static string EvaluateExpression(ConsoleCalculator calculator, IConsoleService console, string expression, string basePath, string indent = null)
    {
        indent = indent ?? "  ";
        console.WriteLine();
        console.WriteLine();
        console.Write("Evaluating of operations expression... ");
        var start = console.GetCursor();
        EnsureScrolling(console, ref start.Top); console.WriteLine();
        console.Write($"{indent}Expression: ");
        PrintLineOnBackground(console, expression, ConsoleColor.DarkBlue);

        var path = CompileExpression(calculator, console, expression, basePath, indent);
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        var evalValue = ReflectExpressionType(console, path, expression, indent);
        return evalValue.ToString();
    }

    internal static void RunFitnessTests(ConsoleCalculator calculator, IConsoleService console, string basePath)
    {
        console.WriteLine();
        console.Write("Loading fitness tests... ");
        var refAsm = typeof(SumTests).Assembly;

        var fitnessTypes = refAsm.GetTypes()
            .Where(t => t.Namespace?.Contains(".Fitness") ?? false)
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
        console.ResetColor();
        console.Write("The ");
        PrintColored(console, fitness.Name, ConsoleColor.Yellow);
        console.WriteLine(" class tests:");
        foreach (var name in names)
        {
            console.ResetColor();
            console.Write($"{ConsoleCalculator.PointerIndent}({names.IndexOf(name) + 1}) ");
            PrintColoredLine(console, name, ConsoleColor.Blue);
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
        PrintColoredLine(console, test.Name, ConsoleColor.Blue);

        var assertion = test.GetCustomAttribute<FactAttribute>().DisplayName;
        console.Write($"{indent}Assertion: ");
        PrintLineOnBackground(console, assertion, ConsoleColor.DarkGray);

        var parts = assertion.Split("should return");
        var expected = parts[1].Trim(new char[] { ' ', '\'' });
        console.Write($"{indent}Expected value: ");
        PrintLineOnBackground(console, expected, ConsoleColor.DarkGray);

        var expression = parts[0].Trim(); // C# code
        console.Write($"{indent}C# expression: ");
        PrintLineOnBackground(console, expression, ConsoleColor.DarkBlue);

        console.WriteLine();
        console.Write($"{indent}Running unit test... ");
        try
        {
            var instance = Activator.CreateInstance(fitness);
            test.Invoke(instance, new object[0]);
            PrintSuccess(console, true);
        }
        catch (Exception e)
        {
            PrintSuccess(console, false, e.Message);
        }

        string path = CompileExpression(calculator, console, expression, basePath, indent);
        if (string.IsNullOrEmpty(path))
            return;

        var evalValue = ReflectExpressionType(console, path, expression, indent);

        console.WriteLine($"{indent}Final asserting... ");
        console.Write($"{indent + indent}Expected: ");
        PrintLineOnBackground(console, expected, ConsoleColor.DarkGray);
        console.Write($"{indent + indent}  Actual: ");
        PrintLineOnBackground(console, evalValue.ToString(), ConsoleColor.DarkBlue);
        console.Write($"{indent + indent}Assertion: ");
        bool assert = expected.Equals(evalValue.ToString());
        PrintSuccess(console, assert, assert.ToString());

        console.Write($"Test #{action + 1}... ");
        PrintSuccess(console, assert, assert ? "Passed" : "Failed");
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

    internal static string CompileExpression(ConsoleCalculator calculator, IConsoleService console, string expression, string basePath, string indent = null)
    {
        string baseDir = Path.Combine(basePath, "Roslyn");
        console.WriteLine($"{indent}Compiling C# expression... ");

        var calcAsm = typeof(IBinaryOperation).Assembly;
        var assemblies = new List<Assembly>();
        AddReferencedAssemblies(calcAsm, assemblies);

        string[] references = assemblies.Select(a => a.Location).Distinct().ToArray();
        var csFile = Path.Combine(baseDir, "OperationSample.csharp");
        string csharpFormat = File.ReadAllText(csFile);
        string csharp = csharpFormat.Replace("{0}", expression);
        var timeStamp = DateTime.Now.TimeOfDay.ToString().Replace(":", string.Empty);
        var toFile = Path.Combine(baseDir, $"OperationSample_{timeStamp}.dll");
        string path = calculator.Compile(csharp, toFile, references, indent + indent);
        if (string.IsNullOrEmpty(path))
            PrintSuccess(console, false, indent: indent);

        return path;
    }

    internal static object ReflectExpressionType(IConsoleService console, string dllPath, string expression, string indent = null)
    {
        if (string.IsNullOrEmpty(dllPath))
            return null;

        if (!File.Exists(dllPath))
            return null;

        console.Write($"{indent}Reflecting from {Path.GetFileName(dllPath)}... ");
        var operationAsm = Assembly.LoadFrom(dllPath);
        var type = operationAsm.GetType("Roslyn.OperationSample");
        var run = type.GetMethod("Run");
        console.WriteLine("Done");

        console.Write($"{indent}Evaluating the expression... ");
        var start = console.GetCursor();
        EnsureScrolling(console, ref start.Top); console.WriteLine();
        console.Write($"{indent + indent}Expression: ");
        EnsureScrolling(console, ref start.Top);
        PrintLineOnBackground(console, expression, ConsoleColor.DarkBlue);
        console.Write($"{indent + indent}Getting value... ");
        object evalValue = string.Empty;
        try
        {
            evalValue = run.Invoke(null, new object[0]);
            EnsureScrolling(console, ref start.Top);
            PrintLineOnBackground(console, evalValue.ToString(), ConsoleColor.DarkBlue);
            var current = console.GetCursor();
            console.SetCursor(start.Left, start.Top);
            console.Write("Done");
            console.SetCursor(current.Left, current.Top);
        }
        catch (Exception e)
        {
            PrintSuccess(console, false, e.Message);
        }
        return evalValue;
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

    private static void PrintColored(IConsoleService console, string text, ConsoleColor color, string indent = null)
    {
        console.Color = color;
        console.Write(indent + text);
        console.ResetColor();
    }
    private static void PrintColoredLine(IConsoleService console, string text, ConsoleColor color, string indent = null)
    {
        PrintColored(console, text, color, indent);
        console.WriteLine();
    }

    private static void PrintOnBackground(IConsoleService console, string text, ConsoleColor color, string indent = null)
    {
        console.Background = color;
        console.Write(indent + text);
        var pos = console.GetCursor();
        console.ResetColor();
        console.Write(' ');
        console.SetCursor(pos.Left, pos.Top);
    }
    private static void PrintLineOnBackground(IConsoleService console, string text, ConsoleColor color, string indent = null)
    {
        PrintOnBackground(console, text, color, indent);
        console.WriteLine();
    }
    private static void EnsureScrolling(IConsoleService console, ref int top)
    {
        if (console.CursorTop >= console.BufferHeight - 1)
            top--;
    }

    public static string ReadLine(IConsoleService console, string defaultText, string caret = "> ")
    {
        List<char> buffer = defaultText.ToCharArray().Take(console.WindowWidth - caret.Length - 1).ToList();
        console.Color = ConsoleColor.DarkBlue;
        console.Write(caret);
        console.Color = ConsoleColor.Blue;
        console.Write(new string(buffer.ToArray()));

        ConsoleKeyInfo info = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);
        while (info.Key != ConsoleKey.Enter)
        {
            info = console.ReadKey(true);
            switch (info.Key)
            {
                case ConsoleKey.Enter:
                    continue;
                case ConsoleKey.LeftArrow:
                    console.SetCursor(Math.Max(console.CursorLeft - 1, caret.Length), console.CursorTop);
                    break;
                case ConsoleKey.RightArrow:
                    console.SetCursor(Math.Min(console.CursorLeft + 1, caret.Length + buffer.Count), console.CursorTop);
                    break;
                case ConsoleKey.Home:
                    console.SetCursor(caret.Length, console.CursorTop);
                    break;
                case ConsoleKey.End:
                    console.SetCursor(caret.Length + buffer.Count, console.CursorTop);
                    break;
                case ConsoleKey.Backspace:
                    if (console.CursorLeft <= caret.Length)
                        break;
                    var cursorColumnAfterBackspace = Math.Max(console.CursorLeft - 1, caret.Length);
                    buffer.RemoveAt(console.CursorLeft - caret.Length - 1);
                    RewriteLine(console, caret, buffer);
                    console.SetCursor(cursorColumnAfterBackspace, console.CursorTop);
                    break;
                case ConsoleKey.Delete:
                    if (console.CursorLeft >= caret.Length + buffer.Count)
                        break;
                    var cursorColumnAfterDelete = console.CursorLeft;
                    buffer.RemoveAt(console.CursorLeft - caret.Length);
                    RewriteLine(console, caret, buffer);
                    console.SetCursor(cursorColumnAfterDelete, console.CursorTop);
                    break;
                default:
                    var character = info.KeyChar;
                    if (character < 32) // not a printable chars
                        break;
                    var cursorAfterNewChar = console.CursorLeft + 1;
                    if (cursorAfterNewChar > console.WindowWidth || caret.Length + buffer.Count >= console.WindowWidth - 1)
                        break; // currently only one line of input is supported
                    buffer.Insert(console.CursorLeft - caret.Length, character);
                    RewriteLine(console, caret, buffer);
                    console.SetCursor(cursorAfterNewChar, console.CursorTop);
                    break;
            }
        }
        console.Write(Environment.NewLine);
        return new string(buffer.ToArray());
    }

    private static void RewriteLine(IConsoleService console, string caret, List<char> buffer)
    {
        console.SetCursor(0, console.CursorTop);
        console.Write(new string(' ', console.WindowWidth - 1));
        console.SetCursor(0, console.CursorTop);
        console.Color = ConsoleColor.DarkBlue;
        console.Write(caret);
        console.Color = ConsoleColor.Blue;
        console.Write(new string(buffer.ToArray()));
    }
}
