using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Faculty : UnaryOperation<long>
    {
        private Faculty() { }

        public Faculty(Operation operation)
            : base(operation) { }

        protected override char Operator => '!';

        public override long Apply(long operand)
            => (operand <= 1) ? 1
                : Apply(operand - 1) * operand;
    }
}
