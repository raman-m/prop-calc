namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Defines binary or unary operation with a parent.
/// </summary>
public interface IOperation : IResultant, IPrintable
{
    IOperation Parent { get; set; }
}
