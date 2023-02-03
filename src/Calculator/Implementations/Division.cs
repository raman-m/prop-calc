using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Division : BinaryOperation<double>, IResultant<int>, IResultant<long>
{
    private Division()
        : base() { }

    public Division(double left, double right)
        : base(left, right) { }

    public Division(IOperation<double> left, double right)
        : base(left, right) { }

    public Division(double left, IOperation<double> right)
        : base(left, right) { }

    public Division(IOperation left, IOperation right)
        : base(left, right) { }

    public Division(IOperation<double> left, IOperation<double> right)
        : base(left, right) { }

    protected override char Operator => '/';

    public override double Apply(double left, double right)
        => left / right;

    protected override string SentenceFormat()
        => nameof(Division).ToLower() + " of {0} by {1}";

    int IResultant<int>.ToResult()
        => Convert.ToInt32(ToResult());

    long IResultant<long>.ToResult()
        => Convert.ToInt64(ToResult());
}
