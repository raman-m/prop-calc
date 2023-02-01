using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Faculty : IUnaryOperation
{
    private long? value;

    private Faculty()
    {
        value = null;
        Operand = null;
        Parent = null;
    }

    public Faculty(int operand)
        : this()
    {
        Operand = new Constant(operand);
        Operand.Parent = this;
    }

    public Faculty(IOperation operation)
        : this()
    {
        Operand = operation;
        Operand.Parent = this;
    }

    public IOperation Operand { get; private set; }

    public IOperation Parent { get; set; }

    public string Print()
    {
        var operand = Operand.Print();
        double val = value.HasValue ? value.Value : ToResult();
        var result = Parent == null ? $" = {val}" : string.Empty;

        return $"({operand}!){result}";
    }

    public string PrintSentence()
    {
        var operand = Operand.PrintSentence();
        double val = value.HasValue ? value.Value : ToResult();
        var result = Parent == null ? $" is {val}" : string.Empty;

        return $"{nameof(Faculty).ToLower()} of {operand}{result}";
    }

    public double ToResult()
    {
        if (!value.HasValue)
        {
            int opr = (int)Operand.ToResult();
            value = Apply(opr);
        }

        return (double)value;
    }

    public static long Apply(int operand)
    {
        if (operand <= 1)
            return 1; // neutral element

        return Apply(operand - 1) * operand;
    }
}
