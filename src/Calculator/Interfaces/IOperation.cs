namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Unary operation with one operand.
/// </summary>
/// <remarks>
/// The value of an operation is caclulative.
/// </remarks>
public interface IOperation : IResultant, IPrintable
{
    IOperation Parent { get; }
    IOperation Operand { get; }

    double? Value { get; }
}
