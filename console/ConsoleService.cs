using RamanM.Properti.Calculator.Console.Interfaces;
using Cns = System.Console;

namespace RamanM.Properti.Calculator.Console;

public class ConsoleService : IConsoleService
{
    public ConsoleColor Color { get => Cns.ForegroundColor; set => Cns.ForegroundColor = value; }
    public ConsoleColor Background { get => Cns.BackgroundColor; set => Cns.BackgroundColor = value; }
    public void ResetColor() => Cns.ResetColor();

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1416 // Validate platform compatibility
    public int BufferWidth { get => Cns.BufferWidth; set => Cns.BufferWidth = value; }
    public int BufferHeight { get => Cns.BufferHeight; set => Cns.BufferHeight = value; }

    public int CursorLeft { get => Cns.CursorLeft; set => Cns.CursorLeft = value; }
    public int CursorTop { get => Cns.CursorTop; set => Cns.CursorTop = value; }

    public bool CursorVisible { get => Cns.CursorVisible; set => Cns.CursorVisible = value; }
    public int WindowWidth { get => Cns.WindowWidth; set => Cns.WindowWidth = value; }

    public void Beep() => Cns.Beep();
    public void Beep(int frequency, int duration) => Cns.Beep(frequency, duration);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning restore IDE0079 // Remove unnecessary suppression

    public void Clear() => Cns.Clear();

    public (int Left, int Top) GetCursor() => Cns.GetCursorPosition();

    public ConsoleKeyInfo ReadKey() => Cns.ReadKey();
    public ConsoleKeyInfo ReadKey(bool intercept) => Cns.ReadKey(true);

    public string ReadLine() => Cns.ReadLine() ?? string.Empty;

    public void SetCursor(int left, int top) => Cns.SetCursorPosition(left, top);

    public void Write(string value) => Cns.Write(value);

    public void Write(object value) => Cns.Write(value);

    public void WriteLine() => Cns.WriteLine();

    public void WriteLine(string value) => Cns.WriteLine(value);

    public void WriteLine(object value) => Cns.WriteLine(value);
}
