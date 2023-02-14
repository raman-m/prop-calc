using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations
{
    public abstract class BinaryOperation<T> : BinaryOperation<T, T>, IBinaryOperation<T>
        where T : struct
    {
        protected BinaryOperation()
            : base() { }

        //public BinaryOperation(T left, T right)
        //    : this()
        //{
        //    Left = new Constant<T>(left, this);
        //    Right = new Constant<T>(right, this);
        //}

        //public BinaryOperation(IOperation left, IOperation right)
        //    : this()
        //{
        //    Left = left;
        //    Right = right;
        //    left.Parent = right.Parent = this;
        //}

        //public BinaryOperation(IOperation left, T right)
        //    : this()
        //{
        //    Left = left;
        //    Left.Parent = this;
        //    Right = new Constant<T>(right, this);
        //}

        //public BinaryOperation(T left, IOperation right)
        //    : this()
        //{
        //    Left = new Constant<T>(left, this);
        //    Right = right;
        //    Right.Parent = this;
        //}

        //public BinaryOperation(IOperation<T> left, IOperation<T> right)
        //    : this((IOperation)left, right)
        //{ }
        public BinaryOperation(T left, T right)
            : this()
        {
            Left = new Operation<T>(left, this);
            Right = new Operation<T>(right, this);
        }

        //public UnaryOperation(Operation operation)
        //    : this()
        //{
        //    Operand = operation;
        //    Operand.Parent = this;
        //}
        //public BinaryOperation(Operation left, Operation right)
        //    : this()
        //{
        //    Left = left;
        //    Right = right;
        //    left.Parent = right.Parent = this;
        //}

        //public UnaryOperation(Operation<T> operation)
        //    : this()
        //{
        //    Operand = operation;
        //    Operand.Parent = this;
        //}
        public BinaryOperation(Operation left, Operation right)
            : this()
        {
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }

        //public BinaryOperation(Operation<T> left, Operation<T> right)
        //    : this()
        //{
        //    Left = left;
        //    Right = right;
        //    left.Parent = right.Parent = this;
        //}


        //protected T? getter;

        //public override IOperation Left { get; protected set; }
        //public override IOperation Right { get; protected set; }
        //public override IOperation Parent { get; set; }

        //public string Print() => Print(ref getter);

        //public string PrintSentence() => PrintSentence(ref getter);

        //public T ToResult() => ToResult(ref getter);
    }

    //public abstract class BinaryOperation<TOut, TIn> : BinaryOperationBase<TIn, TOut>, IBinaryOperation<TOut, TIn>
    //    where TOut : struct
    //    where TIn : struct
    //{
    //    protected BinaryOperation()
    //    {
    //        getter = null;
    //        Left = null;
    //        Right = null;
    //        Parent = null;
    //    }

    //    public BinaryOperation(TIn left, TIn right)
    //        : this()
    //    {
    //        Left = new Constant<TIn>(left, this);
    //        Right = new Constant<TIn>(right, this);
    //    }

    //    //public BinaryOperation(IOperation left, IOperation right)
    //    //    : this()
    //    //{
    //    //    Left = left;
    //    //    Right = right;
    //    //    left.Parent = right.Parent = this;
    //    //}

    //    //public BinaryOperation(IOperation left, TIn right)
    //    //    : this()
    //    //{
    //    //    Left = left;
    //    //    Left.Parent = this;
    //    //    Right = new Constant<TIn>(right, this);
    //    //}

    //    //public BinaryOperation(TIn left, IOperation right)
    //    //    : this()
    //    //{
    //    //    Left = new Constant<TIn>(left, this);
    //    //    Right = right;
    //    //    Right.Parent = this;
    //    //}

    //    //public BinaryOperation(IOperation<TIn> left, IOperation<TIn> right)
    //    //    : this((IOperation)left, right)
    //    //{ }

    //    //protected TOut? getter;

    //    public override IOperation Left { get; protected set; }
    //    public override IOperation Right { get; protected set; }
    //    public override IOperation Parent { get; set; }

    //    public string Print() => Print(ref getter);

    //    public string PrintSentence() => PrintSentence(ref getter);

    //    public object ToResult() => ToResult(ref getter);

    //    TOut IResultant<TOut>.ToResult() => ToResult(ref getter);

    //    public override string ToString() => ToResult().ToString();
    //}

    public abstract class BinaryOperation<TIn, TOut> : Operation<TOut>, IBinaryOperation<TOut, TIn>
        where TIn : struct
        where TOut : struct
    {
        protected BinaryOperation()
        {
            getter = new Lazy<object>(() => ToResult()); //new Lazy<TOut>(ToResult);
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

        public override string Print() //(ref TOut? getter)
        {
            var left = Left.Print();
            var right = Right.Print();
            var format = PrintFormat();
            if (Parent == null)
            {
                format += " = {3}";
                TOut val = Result(); //getter.HasValue ? getter.Value : ToResult(ref getter);
                return string.Format(format, left, right, Operator, val); // (2 + 3) = 5
            }
            return string.Format(format, left, right, Operator); // (2 + 3)
        }

        public override string PrintSentence() //(ref TOut? getter)
        {
            var left = Left.PrintSentence();
            var right = Right.PrintSentence();
            var format = SentenceFormat();
            if (Parent == null)
            {
                TOut val = Result(); //getter.HasValue ? getter.Value : ToResult(ref getter);
                format += " is {2}";
                return string.Format(format, left, right, val);
            }
            return string.Format(format, left, right);
        }

        public override /*TOut*/ object ToResult() //(ref TOut? getter)
        {
            //if (!getter.HasValue)
            //{
            TOut result = Result();
            //getter = Apply(l, r);
            //}
            //return getter.Value;
            return result;
        }

        private TOut Result()
        {
            var lv = Left.ToResult();
            var rv = Right.ToResult();
            TIn l = (TIn)Convert.ChangeType(lv, typeof(TIn)); //((IResultant<TIn>)Left).ToResult();
            TIn r = (TIn)Convert.ChangeType(rv, typeof(TIn)); //((IResultant<TIn>)Right).ToResult();
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
