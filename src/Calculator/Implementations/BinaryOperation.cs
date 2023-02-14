using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public abstract class BinaryOperation<T> : BinaryOperation<T, T>, IBinaryOperation<T>
        where T : struct
    {
        protected BinaryOperation()
            : base() { }

        public BinaryOperation(T left, T right)
            : this()
        {
            Left = new Operation<T>(left, this);
            Right = new Operation<T>(right, this);
        }

        public BinaryOperation(Operation left, Operation right)
            : this()
        {
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }
    }

    public abstract class BinaryOperation<TIn, TOut> : Operation<TOut>, IBinaryOperation<TOut, TIn>
        where TIn : struct
        where TOut : struct
    {
        protected BinaryOperation()
        {
            getter = new Lazy<object>(() => ToResult());
            Left = Right = null;
            Parent = null;
        }

        public BinaryOperation(TIn left, TIn right)
            : this()
        {
            Left = new Constant<TIn>(left, this);
            Right = new Constant<TIn>(right, this);
        }

        public BinaryOperation(Operation left, Operation right)
            : this()
        {
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }

        public IOperation Left { get; protected set; }
        public IOperation Right { get; protected set; }

        protected abstract char Operator { get; }
        public abstract TOut Apply(TIn left, TIn right);

        public override string Print()
        {
            var left = Left.Print();
            var right = Right.Print();
            var format = PrintFormat();
            if (Parent == null)
            {
                format += " = {3}";
                TOut val = Result();
                return string.Format(format, left, right, Operator, val); // (2 + 3) = 5
            }
            return string.Format(format, left, right, Operator); // (2 + 3)
        }

        public override string PrintSentence()
        {
            var left = Left.PrintSentence();
            var right = Right.PrintSentence();
            var format = SentenceFormat();
            if (Parent == null)
            {
                TOut val = Result();
                format += " is {2}";
                return string.Format(format, left, right, val);
            }
            return string.Format(format, left, right);
        }

        public override object ToResult()
        {
            TOut result = Result();
            return result;
        }

        private TOut Result()
        {
            var lv = Left.ToResult();
            var rv = Right.ToResult();
            TIn l = (TIn)Convert.ChangeType(lv, typeof(TIn));
            TIn r = (TIn)Convert.ChangeType(rv, typeof(TIn));
            return Apply(l, r);
        }

        /// <summary>
        /// Gets default print format string as "({0} {2} {1})" <br/> where args are [left, right, operator].
        /// </summary>
        /// <remarks>
        /// Example: (x opr y) <br/> where 'x' is left operand, 'opr' is operator, 'y' is right operand. 
        /// </remarks>
        /// <returns>A string of format.</returns>
        protected virtual string PrintFormat() => "({0} {2} {1})"; // (x opr y)

        /// <summary>
        /// Gets default sentence format string as: {type name} + "of {0} and {1}"
        /// </summary>
        /// <returns>A string of format.</returns>
        protected virtual string SentenceFormat()
        {
            var t = GetType();
            return t.Name.ToLower() + " of {0} and {1}";
        }

        object IResultant.ToResult() => ToResult();

        public override string ToString() => ToResult().ToString();

    }
}
