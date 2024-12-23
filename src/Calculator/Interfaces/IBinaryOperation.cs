namespace RamanM.Properti.Calculator.Interfaces;

public interface IBinaryOperation : IOperation, IResultant, IPrintable
{
    IOperation? Left { get; }
    IOperation? Right { get; }
}

/// <summary>
/// Defines binary operation with two operands.
/// </summary>
public interface IBinaryOperation<T> : IBinaryOperation, IOperation<T>, IResultant<T>, IPrintable
    where T : struct
{
    /// <summary>
    /// Applies the operation.
    /// </summary>
    /// <param name="left">First, left operand.</param>
    /// <param name="right">Second, right operand.</param>
    /// <returns>A value of <typeparamref name="T"/> structure.</returns>
    T Apply(T left, T right);
}

public interface IBinaryOperation<TOut, TIn> : IBinaryOperation, IOperation, IResultant<TOut>, IPrintable
    where TIn : struct
    where TOut : struct
{
    TOut Apply(TIn left, TIn right);
}
