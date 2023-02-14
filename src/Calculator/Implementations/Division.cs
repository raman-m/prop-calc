using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Division : BinaryOperation<double>
    {
        private Division() { }

        public Division(Operation left, Operation right)
            : base(left, right) { }

        protected override char Operator => '/';

        public override double Apply(double left, double right)
            => left / right;

        protected override string SentenceFormat()
            => nameof(Division).ToLower() + " of {0} by {1}";

        public static explicit operator int(Division operation)
            => Convert.ToInt32(operation.ToResult());

        public static explicit operator long(Division operation)
            => Convert.ToInt64(operation.ToResult());
    }
}
