namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Defines binary or unary operation with a parent.
/// </summary>
public interface IOperation : IResultant, IPrintable
{
    IOperation? Parent { get; set; }
}

public interface IOperation<TResult> : IOperation, IResultant<TResult>, IPrintable
    where TResult : struct
{
}

public interface IOperation<TResult, TParent> : IResultant<TResult>, IPrintable
    where TResult : struct
    where TParent : struct
{
    IOperation<TParent> Parent { get; set; }
}
