using RamanM.Properti.Calculator.Console.Interfaces;
using Cns = System.Console;

namespace RamanM.Properti.Calculator.Console;

public class ConsoleService : IConsoleService
{
    public ConsoleColor Color
    {
        get => Cns.ForegroundColor;
        set => Cns.ForegroundColor = value;
    }

    public void Clear() => Cns.Clear();

    public (int Left, int Top) GetCursor() => Cns.GetCursorPosition();

    public ConsoleKeyInfo ReadKey() => Cns.ReadKey();

    public string ReadLine() => Cns.ReadLine();

    public void SetCursor(int left, int top) => Cns.SetCursorPosition(left, top);

    public void Write(string value) => Cns.Write(value);

    public void Write(object value) => Cns.Write(value);

    public void WriteLine() => Cns.WriteLine();

    public void WriteLine(string value) => Cns.WriteLine(value);

    public void WriteLine(object value) => Cns.WriteLine(value);
}
