using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations;

public abstract class UnaryOperation<T> : IUnaryOperation<T>
    where T : struct
{
    protected T? value;

    protected UnaryOperation()
    {
        value = null;
        Operand = null;
        Parent = null;
    }

    public UnaryOperation(T operand)
        : this()
    {
        Operand = new Constant<T>(operand, this);
    }

    public UnaryOperation(IOperation<T> operation)
        : this()
    {
        Operand = operation;
        Operand.Parent = this;
    }

    public UnaryOperation(IOperation operation)
        : this()
    {
        Operand = operation;
        Operand.Parent = this;
    }

    public IOperation Operand { get; private set; }

    public IOperation Parent { get; set; }

    public string Print()
    {
        var operand = Operand.Print();
        var format = PrintFormat();
        if (Parent == null)
        {
            format += " = {2}";
            T val = value.HasValue ? value.Value : ToResult();
            return string.Format(format, operand, Operator, val); // (x!) = y
        }
        return string.Format(format, operand, Operator); // (x!)
    }

    /// <summary>
    /// Gets default print format string as "({0}{1})" <br/> where args are [operand, operator].
    /// </summary>
    /// <remarks>
    /// Example: (x!) <br/> where 'x' is operand, '!' is operator. 
    /// </remarks>
    /// <returns>A string of format.</returns>
    protected virtual string PrintFormat() => "({0}{1})"; // (x!)

    public string PrintSentence()
    {
        var operand = Operand.PrintSentence();
        var format = SentenceFormat();
        if (Parent == null)
        {
            T val = value.HasValue ? value.Value : ToResult();
            format += " is {1}";
            return string.Format(format, operand, val);
        }
        return string.Format(format, operand);
    }

    /// <summary>
    /// Gets default sentence format string as: {type name} + "of {0}" where index 0 is operand
    /// </summary>
    /// <returns>A string of format.</returns>
    protected virtual string SentenceFormat()
    {
        var t = GetType();
        return t.Name.ToLower() + " of {0}";
    }

    public T ToResult()
    {
        if (!value.HasValue)
        {
            T result = ((IResultant<T>)Operand).ToResult();
            value = Apply(result);
        }
        return value.Value;
    }

    protected abstract char Operator { get; }
    public abstract T Apply(T operand);

    object IResultant.ToResult() => ToResult();
}
