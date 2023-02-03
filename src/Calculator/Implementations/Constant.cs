using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public class Constant : IOperation
{
    private readonly object value;

    private Constant() { }

    public Constant(object value)
    {
        this.value = value;
        Parent = null;
    }

    public Constant(object value, IOperation parent)
    {
        this.value = value;
        Parent = parent;
    }

    public IOperation Parent { get; set; }

    public string Print() => value.ToString();

    public string PrintSentence() => value.ToString();

    public object ToResult() => value;
}

public class Constant<T> : IOperation<T>
    where T : struct
{
    private readonly T value;

    private Constant() { }

    public Constant(object value)
    {
        object v = Convert.ChangeType(value, typeof(T));
        this.value = (T)v;
        Parent = null;
    }

    public Constant(object value, IOperation parent)
        : this(value)
    {
        Parent = parent;
    }

    public Constant(object value, IOperation<T> parent)
        : this(value)
    {
        Parent = (IOperation)parent;
    }

    public Constant(T value)
    {
        this.value = value;
        Parent = null;
    }

    public Constant(T value, IOperation parent)
    {
        this.value = value;
        Parent = parent;
    }

    public Constant(T value, IOperation<T> parent)
    {
        this.value = value;
        Parent = (IOperation)parent;
    }

    public IOperation Parent { get; set; }

    public string Print() => value.ToString();

    public string PrintSentence() => value.ToString();

    public T ToResult() => value;

    object IResultant.ToResult() => ToResult();
}
