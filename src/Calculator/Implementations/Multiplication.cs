using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Multiplication : BinaryOperation<double>, IResultant<int>, IResultant<long>
    {
        private Multiplication()
            : base() { }

        public Multiplication(double left, double right)
            : base(left, right) { }

        public Multiplication(IOperation<double> left, double right)
            : base(left, right) { }

        public Multiplication(double left, IOperation<double> right)
            : base(left, right) { }

        public Multiplication(IOperation left, IOperation right)
            : base(left, right) { }

        public Multiplication(IOperation<double> left, IOperation<double> right)
            : base(left, right) { }

        protected override char Operator => '*';

        public override double Apply(double left, double right)
            => left * right;

        int IResultant<int>.ToResult()
            => Convert.ToInt32(ToResult());

        long IResultant<long>.ToResult()
            => Convert.ToInt64(ToResult());
    }
}
