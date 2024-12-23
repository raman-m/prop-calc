using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Operation : IOperation
{
    protected Lazy<object> getter;

    protected Operation() { }

    public Operation(double value)
    {
        getter = new Lazy<object>(() => value);
    }
    public Operation(double value, IOperation parent)
        : this(value)
    {
        Parent = parent;
    }

    public Operation(Lazy<object> getter)
    {
        this.getter = getter;
    }
    public Operation(Lazy<object> getter, IOperation parent)
        : this(getter)
    {
        Parent = parent;
    }

    public Operation(Func<object> getter)
    {
        this.getter = new Lazy<object>(getter);
    }
    public Operation(Func<object> getter, IOperation parent)
        : this(getter)
    {
        Parent = parent;
    }

    public IOperation Parent { get; set; }

    public virtual object ToResult() => getter.Value;

    public virtual string Print() => ToString();

    public virtual string PrintSentence() => ToString();

    public override string ToString() => ToResult().ToString();

    public static implicit operator Operation(double constant) => new Constant<double>(constant);
    public static implicit operator Operation(int constant) => new Constant<int>(constant);
    public static implicit operator Operation(long constant) => new Constant<long>(constant);
}

public class Operation<T> : Operation, IOperation<T>, IOperation
    where T : struct
{
    protected Operation() { }

    public Operation(double value)
    {
        getter = new Lazy<object>(() => value);
    }

    public Operation(T value)
    {
        getter = new Lazy<object>(() => value);
    }
    public Operation(T value, IOperation parent)
        : this(value)
    {
        Parent = parent;
    }

    public Operation(Lazy<T> getter)
    {
        base.getter = new Lazy<object>(() => getter.Value);
    }
    public Operation(Lazy<T> getter, IOperation parent)
        : this(getter)
    {
        Parent = parent;
    }

    public Operation(Func<T> getter)
    {
        base.getter = new Lazy<object>(() => getter.Invoke());
    }
    public Operation(Func<T> getter, IOperation parent)
        : this(getter)
    {
        Parent = parent;
    }

    T IResultant<T>.ToResult()
    {
        return (T)ToResult();
    }
}
