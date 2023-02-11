using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public class Fraction : BinaryOperation<double, int> //, IResultant<int>, IResultant<long>
    {
        private Fraction()
            : base() { }

        public Fraction(int left, int right)
            : base(left, right) { }

        //public Fraction(IOperation left, int right)
        //    : base(left, right) { }
        //public Fraction(IOperation<int> left, int right)
        //    : base(left, right) { }

        //public Fraction(int left, IOperation right)
        //    : base(left, right) { }
        //public Fraction(int left, IOperation<int> right)
        //    : base(left, right) { }

        //public Fraction(IOperation left, IOperation right)
        //    : base(left, right) { }
        //public Fraction(IOperation<int> left, IOperation<int> right)
        //    : base(left, right) { }

        protected override char Operator => '/';

        public override double Apply(int left, int right)
            => (double)left / right;

        protected override string PrintFormat()
            => base.PrintFormat().Replace(" ", string.Empty);

        protected override string SentenceFormat()
            => "{0}" + Operator + "{1}";

        //int IResultant<int>.ToResult()
        //    => Convert.ToInt32(ToResult());
        public static implicit operator int(Fraction operation)
            => Convert.ToInt32(operation.ToResult());

        //long IResultant<long>.ToResult()
        //    => Convert.ToInt64(ToResult());
        public static implicit operator long(Fraction operation)
            => Convert.ToInt64(operation.ToResult());

        public static implicit operator double(Fraction operation)
            => (double)operation.ToResult();
        //public static implicit operator double(Fraction operation)
        //    => ((IResultant<double>)operation).ToResult();
    }
}
