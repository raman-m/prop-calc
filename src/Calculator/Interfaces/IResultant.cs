namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Defines the method for resulting to <typeparamref name="TOut"/> value.
/// </summary>
/// <typeparam name="TOut">Type of resulting structure value.</typeparam>
public interface IResultant<TOut>
    where TOut : struct
{
    /// <summary>
    /// Makes a result being represented as <typeparamref name="TOut"/> value.
    /// </summary>
    /// <returns>A value of the <typeparamref name="TOut"/> structure.</returns>
    TOut ToResult();
}

public interface IResultant
{
    object ToResult();
}
