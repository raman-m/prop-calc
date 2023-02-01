using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Constant : IOperation
{
    private readonly double value;

    private Constant()
    { }

    public Constant(double value)
    {
        this.value = value;
        Parent = null;
        Operand = null;
    }

    public IOperation Parent { get; set; }

    public IOperation Operand { get; }

    public string Print()
    {
        return value.ToString();
    }

    public string PrintSentence()
    {
        return Print();
    }

    public double ToResult()
    {
        return value;
    }
}
