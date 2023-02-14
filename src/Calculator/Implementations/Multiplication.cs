using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Multiplication : BinaryOperation<double> //, IResultant<int>, IResultant<long>
    {
        private Multiplication() { }

        //public Multiplication(double left, double right)
        //    : base(left, right) { }

        //public Multiplication(IOperation left, double right)
        //    : base(left, right) { }
        //public Multiplication(IOperation<double> left, double right)
        //    : base(left, right) { }

        //public Multiplication(double left, IOperation right)
        //    : base(left, right) { }
        //public Multiplication(double left, IOperation<double> right)
        //    : base(left, right) { }

        //public Multiplication(IOperation left, IOperation right)
        //    : base(left, right) { }

        public Multiplication(Operation left, Operation right)
            : base(left, right) { }

        protected override char Operator => '*';

        public override double Apply(double left, double right)
            => left * right;

        //int IResultant<int>.ToResult()
        //    => Convert.ToInt32(ToResult());
        public static explicit operator int(Multiplication operation)
            => Convert.ToInt32(operation.ToResult());

        //long IResultant<long>.ToResult()
        //    => Convert.ToInt64(ToResult());
        public static explicit operator long(Multiplication operation)
            => Convert.ToInt64(operation.ToResult());

        //public static implicit operator double(Multiplication operation)
        //    => operation.ToResult();
    }
}
