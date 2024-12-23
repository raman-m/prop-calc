using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator;


public class Calculator
{
    protected readonly IConsoleWriting @out;

    public Calculator(IConsoleWriting output)
    {
        @out = output;
    }

    public double Sum(double left, double right)
    {
        return left + right;
    }
}
