using RamanM.Properti.Calculator.Interfaces;
using System;

namespace RamanM.Properti.Calculator.Implementations;

//public class Constant //: Constant<double>, IOperation<double>, IOperation
//{
//}

//public class Constant : Operation, IOperation, IResultant //, IResultant<double>, IResultant<int>, IResultant<long>
//{
//    private readonly object value;

//    private Constant() { }

//    //public Constant(object getter)
//    //{
//    //    object v = Convert.ChangeType(getter, typeof(T));
//    //    this.getter = (T)v;
//    //    Parent = null;
//    //}

//    //public Constant(object getter, IOperation parent)
//    //    : this(getter)
//    //{
//    //    Parent = parent;
//    //}

//    //public Constant(object getter, IOperation<T> parent)
//    //    : this(getter)
//    //{
//    //    Parent = (IOperation)parent;
//    //}

//    public Constant(object value) //: base(value) { }
//    {
//        this.value = value;
//    }

//    public Constant(object value, IOperation parent) //: base(value, parent) { }
//    {
//        this.value = value;
//        Parent = parent;
//    }

//    public override string Print() => ToString();
//    public override string PrintSentence() => ToString();

//    public override object ToResult() => value;
//    //double IResultant.ToResult() => ToResult();

//    public override string ToString() => value.ToString();

//    //public static implicit operator Constant<T>(double constant) => new Constant<double>(constant);
//    //public static implicit operator Constant<T>(int constant) => new Constant<int>(constant);
//    //public static implicit operator Constant<T>(long constant) => new Constant<long>(constant);
//}

public class Constant<T> : Operation, IOperation<T>, IOperation, IResultant //, IResultant<double>, IResultant<int>, IResultant<long>
    where T : struct
{
    private readonly /*object*/ T value;

    private Constant() { }

    public Constant(T value) //: base(value) { }
    {
        this.value = value;
    }

    public Constant(T value, IOperation parent) //: base(value, parent) { }
    {
        this.value = value;
        Parent = parent;
    }

    public override string Print() => ToString();
    public override string PrintSentence() => ToString();

    public override object ToResult() => value;
    //double IResultant.ToResult() => ToResult();

    public override string ToString() => value.ToString();

    T IResultant<T>.ToResult() => value;

    //public static implicit operator Constant<T>(double constant) => new Constant<double>(constant);
    //public static implicit operator Constant<T>(int constant) => new Constant<int>(constant);
    //public static implicit operator Constant<T>(long constant) => new Constant<long>(constant);
}
