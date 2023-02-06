using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Console.Interfaces
{
    public interface IConsoleService : IConsoleWriting
    {
        ConsoleKeyInfo ReadKey();
        ConsoleKeyInfo ReadKey(bool intercept);
        string ReadLine();
        void Clear();

        ConsoleColor Color { get; set; }
        (int Left, int Top) GetCursor();
        void SetCursor(int left, int top);

        int BufferWidth { get; set; }
        int BufferHeight { get; set; }

        int CursorLeft { get; set; }
        int CursorTop { get; set; }

        void Beep();
        void Beep(int frequency, int duration);

        bool CursorVisible { get; set; }
    }
}
