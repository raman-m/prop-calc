using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Fraction : IBinaryOperation
{
    private double? value;

    private Fraction()
    {
        value = null;
        Left = null;
        Right = null;
        Parent = null;
    }

    public Fraction(int left, int right)
        : this()
    {
        Left = new Constant(left);
        Right = new Constant(right);
        Left.Parent = Right.Parent = this;
    }

    public Fraction(IOperation left, int right)
        : this()
    {
        Left = left;
        Right = new Constant(right);
        Left.Parent = Right.Parent = this;
    }

    public Fraction(int left, IOperation right)
        : this()
    {
        Left = new Constant(left);
        Right = right;
        Left.Parent = Right.Parent = this;
    }

    public Fraction(IOperation left, IOperation right)
        : this()
    {
        Left = left;
        Right = right;
        left.Parent = right.Parent = this;
    }

    public IOperation Left { get; private set; }

    public IOperation Right { get; private set; }

    public IOperation Parent { get; set; }

    public string Print()
    {
        var left = Left.Print();
        var right = Right.Print();
        double val = value.HasValue ? value.Value : ToResult();
        var result = Parent == null ? $" = {val}" : string.Empty;

        return $"({left}/{right}){result}";
    }

    public string PrintSentence()
    {
        var left = Left.PrintSentence();
        var right = Right.PrintSentence();
        double val = value.HasValue ? value.Value : ToResult();
        var result = Parent == null ? $" is {val}" : string.Empty;

        return $"{nameof(Fraction).ToLower()} of {left} by {right}{result}";
    }

    public double ToResult()
    {
        if (!value.HasValue)
        {
            var l = Left.ToResult();
            var r = Right.ToResult();
            value = Apply(l, r);
        }

        return (double)value;
    }
    public static double Apply(double left, double right)
    {
        return left / right;
    }
}
