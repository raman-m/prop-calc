using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    /// <summary>
    /// Subtraction of two operations.
    /// </summary>
    public class Subtraction : BinaryOperation<double> //, IResultant<int>, IResultant<long>
    {
        private Subtraction() { }

        //public Subtraction(double left, double right)
        //    : base(left, right) { }

        //public Subtraction(IOperation left, double right)
        //   : base(left, right) { }
        //public Subtraction(IOperation<double> left, double right)
        //    : base(left, right) { }

        //public Subtraction(double left, IOperation right)
        //    : base(left, right) { }
        //public Subtraction(double left, IOperation<double> right)
        //    : base(left, right) { }

        //public Subtraction(IOperation left, IOperation right)
        //   : base(left, right) { }
        public Subtraction(Operation left, Operation right)
           : base(left, right) { }

        protected override char Operator => '-';

        public override double Apply(double left, double right)
            => left - right;

        //int IResultant<int>.ToResult()
        //    => Convert.ToInt32(ToResult());
        public static explicit operator int(Subtraction operation)
            => Convert.ToInt32(operation.ToResult());

        //long IResultant<long>.ToResult()
        //    => Convert.ToInt64(ToResult());
        public static explicit operator long(Subtraction operation)
            => Convert.ToInt64(operation.ToResult());

        //public static implicit operator double(Subtraction operation)
        //    => operation.ToResult();
    }
}
