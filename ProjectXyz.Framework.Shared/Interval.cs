using System;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Framework.Shared
{
    public sealed class Interval<T> : 
        IInterval<T>
        where T : IComparable<T>
    {
        #region Constructors
        public Interval(T value)
        {
            Value = value;
        }
        #endregion

        #region Properties
        public T Value { get; }
        #endregion

        #region Methods
        public int CompareTo(IInterval<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var casted = obj as IInterval<T>;
            if (casted != null)
            {
                return CompareTo(casted);
            }

            throw new ArgumentException($"Object is not of type '{GetType()}'.");
        }

        public int CompareTo(IInterval other)
        {
            if (other == null)
            {
                return 1;
            }

            var casted = other as IInterval<T>;
            if (casted != null)
            {
                return CompareTo(casted);
            }

            throw new ArgumentException($"Object is not of type '{GetType()}'.");
        }

        public IInterval Add(IInterval<T> other)
        {
            return new Interval<T>((dynamic)Value + (dynamic)other.Value);
        }

        public IInterval Subtract(IInterval<T> other)
        {
            return new Interval<T>((dynamic)Value - (dynamic)other.Value);
        }

        public double Multiply(IInterval<T> other)
        {
            return (dynamic)Value * (dynamic)other.Value;
        }

        public double Divide(IInterval<T> other)
        {
            return (dynamic)Value / (dynamic)other.Value;
        }

        public IInterval Add(IInterval other)
        {
            var casted = other as IInterval<T>;
            if (casted == null)
            {
                throw new ArgumentException($"'{other.GetType()}' must be of type '{this.GetType()}'.");
            }

            return Add(casted);
        }

        public IInterval Subtract(IInterval other)
        {
            var casted = other as IInterval<T>;
            if (casted == null)
            {
                throw new ArgumentException($"'{other.GetType()}' must be of type '{this.GetType()}'.");
            }

            return Subtract(casted);
        }

        public double Multiply(IInterval other)
        {
            var casted = other as IInterval<T>;
            if (casted == null)
            {
                throw new ArgumentException($"'{other.GetType()}' must be of type '{this.GetType()}'.");
            }

            return Multiply(casted);
        }

        public double Divide(IInterval other)
        {
            var casted = other as IInterval<T>;
            if (casted == null)
            {
                throw new ArgumentException($"'{other.GetType()}' must be of type '{this.GetType()}'.");
            }

            return Divide(casted);
        }

        public IInterval Multiply(double multiplier)
        {
            return new Interval<T>((dynamic)Value * multiplier);
        }

        public IInterval Divide(double divisor)
        {
            return new Interval<T>((dynamic)Value / divisor);
        }

        public override string ToString()
        {
            return $"Interval<{typeof(T)}>: {Value}";
        }
        #endregion
    }
}