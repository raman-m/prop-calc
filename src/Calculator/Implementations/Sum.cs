using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    /// <summary>
    /// Summation of two operations.
    /// </summary>
    public class Sum : BinaryOperation<double>, IResultant<int>, IResultant<long>
    {
        private Sum()
            : base() { }

        public Sum(double left, double right)
            : base(left, right) { }

        public Sum(IOperation left, double right)
            : base(left, right) { }
        public Sum(IOperation<double> left, double right)
            : base(left, right) { }

        public Sum(double left, IOperation right)
            : base(left, right) { }
        public Sum(double left, IOperation<double> right)
            : base(left, right) { }

        public Sum(IOperation left, IOperation right)
            : base(left, right) { }
        public Sum(IOperation<double> left, IOperation<double> right)
            : base(left, right) { }

        protected override char Operator => '+';

        public override double Apply(double left, double right)
            => left + right;

        int IResultant<int>.ToResult()
            => Convert.ToInt32(ToResult());

        long IResultant<long>.ToResult()
            => Convert.ToInt64(ToResult());
    }
}
