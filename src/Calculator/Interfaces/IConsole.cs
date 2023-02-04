namespace RamanM.Properti.Calculator.Interfaces;

public interface IConsoleWriting
{
    void Write(string value);
    void Write(object value);

    void WriteLine();
    void WriteLine(string value);
    void WriteLine(object value);
}
