using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Console.Interfaces;

public interface IConsoleService : IConsoleWriting
{
    ConsoleKeyInfo ReadKey();
    string ReadLine();
    void Clear();

    ConsoleColor Color { get; set; }
    (int Left, int Top) GetCursor();
    void SetCursor(int left, int top);
}
