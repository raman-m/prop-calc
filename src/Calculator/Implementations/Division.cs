using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Division : BinaryOperation<double> //, IResultant<int>, IResultant<long>
    {
        private Division() { }

        //public Division(double left, double right)
        //    : base(left, right) { }

        //public Division(IOperation left, double right)
        //    : base(left, right) { }
        //public Division(IOperation<double> left, double right)
        //    : base(left, right) { }

        //public Division(double left, IOperation right)
        //    : base(left, right) { }
        //public Division(double left, IOperation<double> right)
        //    : base(left, right) { }

        //public Division(IOperation left, IOperation right)
        //    : base(left, right) { }
        public Division(Operation left, Operation right)
            : base(left, right) { }

        protected override char Operator => '/';

        public override double Apply(double left, double right)
            => left / right;

        protected override string SentenceFormat()
            => nameof(Division).ToLower() + " of {0} by {1}";

        //int IResultant<int>.ToResult()
        //    => Convert.ToInt32(ToResult());
        public static explicit operator int(Division operation)
            => Convert.ToInt32(operation.ToResult());

        //long IResultant<long>.ToResult()
        //    => Convert.ToInt64(ToResult());
        public static explicit operator long(Division operation)
            => Convert.ToInt64(operation.ToResult());

        //public static implicit operator double(Division operation)
        //    => operation.ToResult();
    }
}
