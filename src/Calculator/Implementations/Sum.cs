﻿using RamanM.Properti.Calculator.Interfaces;
using System;
using System.Reflection.Metadata;

namespace RamanM.Properti.Calculator.Implementations
{
    /// <summary>
    /// Summation of two operations.
    /// </summary>
    public class Sum : BinaryOperation<double> //,IResultant<int>, IResultant<long>
    {
        private Sum() { }

        //public Sum(double left, double right)
        //    : base(left, right) { }

        //public Sum(IOperation left, double right)
        //    : base(left, right) { }
        //public Sum(IOperation<double> left, double right)
        //    : base(left, right) { }

        //public Sum(double left, IOperation right)
        //    : base(left, right) { }
        //public Sum(double left, IOperation<double> right)
        //    : base(left, right) { }

        //public Sum(IOperation left, IOperation right)
        //    : base(left, right) { }

        //public Sum(Constant<double> left, Constant<double> right)
        //{
        //    Left = left;
        //    Right = right;
        //    Parent = null;
        //}

        public Sum(Operation left, Operation right)
            : base(left, right) { }

        protected override char Operator => '+';

        public override double Apply(double left, double right)
            => left + right;

        //int IResultant<int>.ToResult()
        //    => Convert.ToInt32(ToResult());
        public static explicit operator int(Sum operation)
            => Convert.ToInt32(operation.ToResult());

        //long IResultant<long>.ToResult()
        //    => Convert.ToInt64(ToResult());
        public static explicit operator long(Sum operation)
            => Convert.ToInt64(operation.ToResult());

        //public static implicit operator double(Sum operation)
        //    => operation.ToResult();
    }
}
