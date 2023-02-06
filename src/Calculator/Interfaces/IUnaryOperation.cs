namespace RamanM.Properti.Calculator.Interfaces
{
    /// <summary>
    /// Defines unary operation with one operand.
    /// </summary>
    public interface IUnaryOperation<T> : IOperation<T>, IResultant<T>, IOperation, IResultant, IPrintable
        where T : struct
    {
        IOperation Operand { get; }

        T Apply(T operand);
    }
}
