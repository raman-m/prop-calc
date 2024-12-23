namespace RamanM.Properti.Calculator.Implementations;

/// <summary>
/// Summation of two operations.
/// </summary>
public class Sum : BinaryOperation<double>
{
    private Sum() { }

    public Sum(Operation left, Operation right)
        : base(left, right) { }

    protected override char Operator => '+';

    public override double Apply(double left, double right)
        => left + right;

    public static explicit operator int(Sum operation)
        => Convert.ToInt32(operation.ToResult());

    public static explicit operator long(Sum operation)
        => Convert.ToInt64(operation.ToResult());
}
