using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Constant<T> : Operation, IOperation<T>, IOperation, IResultant
    where T : struct
{
    private readonly T value;

    private Constant() { }

    public Constant(T value)
    {
        this.value = value;
    }

    public Constant(T value, IOperation parent)
    {
        this.value = value;
        Parent = parent;
    }

    public override string Print() => ToString();
    public override string PrintSentence() => ToString();
    public override object ToResult() => value;
    public override string ToString() => value.ToString() ?? string.Empty;
    T IResultant<T>.ToResult() => value;
}
