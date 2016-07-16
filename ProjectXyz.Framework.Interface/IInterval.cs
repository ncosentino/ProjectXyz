using System;

namespace ProjectXyz.Framework.Interface
{
    public interface IInterval<T> :
        IInterval,
        IComparable<IInterval<T>>
        where T : IComparable<T>
    {
        T Value { get; }

        IInterval Add(IInterval<T> other);

        IInterval Subtract(IInterval<T> other);

        double Multiply(IInterval<T> other);

        double Divide(IInterval<T> other);
    }

    public interface IInterval : IComparable<IInterval>
    {
        IInterval Add(IInterval other);

        IInterval Subtract(IInterval other);

        double Multiply(IInterval other);

        double Divide(IInterval other);

        IInterval Multiply(double multiplier);

        IInterval Divide(double divisor);
    }
}