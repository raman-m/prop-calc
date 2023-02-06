using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Implementations
{
    public abstract class BinaryOperation<T> : BinaryOperationBase<T, T>, IBinaryOperation<T>
        where T : struct
    {
        protected BinaryOperation()
        {
            value = null;
            Left = null;
            Right = null;
            Parent = null;
        }

        public BinaryOperation(T left, T right)
            : this()
        {
            Left = new Constant<T>(left, this);
            Right = new Constant<T>(right, this);
        }

        public BinaryOperation(IOperation left, IOperation right)
            : this()
        {
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }

        public BinaryOperation(IOperation left, T right)
            : this()
        {
            Left = left;
            Left.Parent = this;
            Right = new Constant<T>(right, this);
        }

        public BinaryOperation(T left, IOperation right)
            : this()
        {
            Left = new Constant<T>(left, this);
            Right = right;
            Right.Parent = this;
        }

        public BinaryOperation(IOperation<T> left, IOperation<T> right)
            : this((IOperation)left, right)
        { }

        protected T? value;

        public override IOperation Left { get; protected set; }
        public override IOperation Right { get; protected set; }
        public override IOperation Parent { get; set; }

        public string Print() => Print(ref value);

        public string PrintSentence() => PrintSentence(ref value);

        public T ToResult() => ToResult(ref value);

        object IResultant.ToResult() => ToResult();
    }

    public abstract class BinaryOperation<TOut, TIn> : BinaryOperationBase<TIn, TOut>, IBinaryOperation<TOut, TIn>
        where TOut : struct
        where TIn : struct
    {
        protected BinaryOperation()
        {
            value = null;
            Left = null;
            Right = null;
            Parent = null;
        }

        public BinaryOperation(TIn left, TIn right)
            : this()
        {
            Left = new Constant<TIn>(left, this);
            Right = new Constant<TIn>(right, this);
        }

        public BinaryOperation(IOperation left, IOperation right)
            : this()
        {
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }

        public BinaryOperation(IOperation left, TIn right)
            : this()
        {
            Left = left;
            Left.Parent = this;
            Right = new Constant<TIn>(right, this);
        }

        public BinaryOperation(TIn left, IOperation right)
            : this()
        {
            Left = new Constant<TIn>(left, this);
            Right = right;
            Right.Parent = this;
        }

        public BinaryOperation(IOperation<TIn> left, IOperation<TIn> right)
            : this((IOperation)left, right)
        { }

        protected TOut? value;

        public override IOperation Left { get; protected set; }
        public override IOperation Right { get; protected set; }
        public override IOperation Parent { get; set; }

        public string Print() => Print(ref value);

        public string PrintSentence() => PrintSentence(ref value);

        public object ToResult() => ToResult(ref value);

        TOut IResultant<TOut>.ToResult() => ToResult(ref value);
    }

    public abstract class BinaryOperationBase<TIn, TOut>
        where TIn : struct
        where TOut : struct
    {
        public abstract IOperation Left { get; protected set; }
        public abstract IOperation Right { get; protected set; }
        public abstract IOperation Parent { get; set; }
        protected abstract char Operator { get; }
        public abstract TOut Apply(TIn left, TIn right);

        protected string Print(ref TOut? value)
        {
            var left = Left.Print();
            var right = Right.Print();
            var format = PrintFormat();
            if (Parent == null)
            {
                format += " = {3}";
                TOut val = value.HasValue ? value.Value : ToResult(ref value);
                return string.Format(format, left, right, Operator, val); // (2 + 3) = 5
            }
            return string.Format(format, left, right, Operator); // (2 + 3)
        }

        protected string PrintSentence(ref TOut? value)
        {
            var left = Left.PrintSentence();
            var right = Right.PrintSentence();
            var format = SentenceFormat();
            if (Parent == null)
            {
                TOut val = value.HasValue ? value.Value : ToResult(ref value);
                format += " is {2}";
                return string.Format(format, left, right, val);
            }
            return string.Format(format, left, right);
        }

        protected TOut ToResult(ref TOut? value)
        {
            if (!value.HasValue)
            {
                TIn l = ((IResultant<TIn>)Left).ToResult();
                TIn r = ((IResultant<TIn>)Right).ToResult();
                value = Apply(l, r);
            }
            return value.Value;
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
    }
}
