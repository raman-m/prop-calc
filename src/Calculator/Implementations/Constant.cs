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
    }

    public IOperation Parent { get; set; }

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
