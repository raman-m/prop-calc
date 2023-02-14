using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Faculty : UnaryOperation<long> //,IResultant<double>, IResultant<int>
    {
        private Faculty() { }

        //public Faculty(long operand)
        //    : base(operand) { }

        //public Faculty(IOperation operation)
        //    : base(operation) { }

        //public Faculty(IOperation<long> operation)
        //    : base(operation) { }

        public Faculty(Operation operation)
            : base(operation) { }

        protected override char Operator => '!';

        public override long Apply(long operand)
            => (operand <= 1) ? 1
                : Apply(operand - 1) * operand;

        //double IResultant<double>.ToResult()
        //    => (double)ToResult();
        //public static explicit operator double(Faculty operation)
        //    => operation.ToResult();

        //int IResultant<int>.ToResult()
        //    => unchecked((int)ToResult());
        //public static explicit operator int(Faculty operation)
        //    => unchecked((int)(operation.ToResult()));
    }
}
