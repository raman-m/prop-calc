using System;

namespace RamanM.Properti.Calculator.Implementations
{
    /// <summary>
    /// Subtraction of two operations.
    /// </summary>
    public class Subtraction : BinaryOperation<double>
    {
        private Subtraction() { }

        public Subtraction(Operation left, Operation right)
           : base(left, right) { }

        protected override char Operator => '-';

        public override double Apply(double left, double right)
            => left - right;

        public static explicit operator int(Subtraction operation)
            => Convert.ToInt32(operation.ToResult());

        public static explicit operator long(Subtraction operation)
            => Convert.ToInt64(operation.ToResult());
    }
}
