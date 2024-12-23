namespace RamanM.Properti.Calculator.Implementations;

public class Multiplication : BinaryOperation<double>
{
    private Multiplication() { }

    public Multiplication(Operation left, Operation right)
        : base(left, right) { }

    protected override char Operator => '*';

    public override double Apply(double left, double right)
        => left * right;

    public static explicit operator int(Multiplication operation)
        => Convert.ToInt32(operation.ToResult());

    public static explicit operator long(Multiplication operation)
        => Convert.ToInt64(operation.ToResult());
}
