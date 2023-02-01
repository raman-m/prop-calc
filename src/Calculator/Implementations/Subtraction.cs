using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

/// <summary>
/// Subtraction of two operations.
/// </summary>
public class Subtraction : IBinaryOperation
{
    private double? value;

    private Subtraction()
    {
        value = null;
        Left = null;
        Right = null;
        Parent = null;
    }

    public Subtraction(double left, double right)
        : this()
    {
        Left = new Constant(left);
        Right = new Constant(right);
        Left.Parent = Right.Parent = this;
    }

    public Subtraction(IOperation left, double right)
        : this()
    {
        Left = left;
        Right = new Constant(right);
        Left.Parent = Right.Parent = this;
    }

    public Subtraction(double left, IOperation right)
        : this()
    {
        Left = new Constant(left);
        Right = right;
        Left.Parent = Right.Parent = this;
    }

    public Subtraction(IOperation left, IOperation right)
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

        return $"({left} - {right}){result}";
    }

    public string PrintSentence()
    {
        var left = Left.PrintSentence();
        var right = Right.PrintSentence();
        double val = value.HasValue ? value.Value : ToResult();
        var result = Parent == null ? $" is {val}" : string.Empty;

        return $"{nameof(Subtraction).ToLower()} of {left} and {right}{result}";
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
        return left - right;
    }
}
