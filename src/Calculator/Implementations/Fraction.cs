namespace RamanM.Properti.Calculator.Implementations;

public class Fraction : BinaryOperation<int, double>
{
    private Fraction() { }

    public Fraction(Operation left, Operation right)
        : base(left, right) { }

    protected override char Operator => '/';

    public override double Apply(int left, int right)
        => (double)left / right;

    protected override string PrintFormat()
        => base.PrintFormat().Replace(" ", string.Empty);

    protected override string SentenceFormat()
        => "{0}" + Operator + "{1}";

    public static explicit operator int(Fraction operation)
        => Convert.ToInt32(operation.ToResult());

    public static explicit operator long(Fraction operation)
        => Convert.ToInt64(operation.ToResult());

    public static explicit operator double(Fraction operation)
        => (double)operation.ToResult();
}
