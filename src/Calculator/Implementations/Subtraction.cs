using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

/// <summary>
/// Subtraction of two operations.
/// </summary>
public class Subtraction : BinaryOperation<double>, IResultant<int>, IResultant<long>
{
    private Subtraction()
        : base() { }

    public Subtraction(double left, double right)
        : base(left, right) { }

    public Subtraction(IOperation<double> left, double right)
        : base(left, right) { }

    public Subtraction(double left, IOperation<double> right)
        : base(left, right) { }

    public Subtraction(IOperation left, IOperation right)
       : base(left, right) { }

    public Subtraction(IOperation<double> left, IOperation<double> right)
       : base(left, right) { }

    protected override char Operator => '-';

    public override double Apply(double left, double right)
        => left - right;

    int IResultant<int>.ToResult()
        => Convert.ToInt32(ToResult());

    long IResultant<long>.ToResult()
        => Convert.ToInt64(ToResult());
}
