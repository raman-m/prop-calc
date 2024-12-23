﻿using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Console.Interfaces;

public interface IConsoleService : IConsoleWriting
{
    ConsoleKeyInfo ReadKey();
    ConsoleKeyInfo ReadKey(bool intercept);
    string ReadLine();
    void Clear();

    ConsoleColor Color { get; set; }
    ConsoleColor Background { get; set; }
    void ResetColor();

    (int Left, int Top) GetCursor();
    void SetCursor(int left, int top);

    int BufferWidth { get; set; }
    int BufferHeight { get; set; }

    int WindowWidth { get; set; }

    int CursorLeft { get; set; }
    int CursorTop { get; set; }

    void Beep();
    void Beep(int frequency, int duration);

    bool CursorVisible { get; set; }
}
