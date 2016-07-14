using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class CalculationPriority<T> :
        ICalculationPriority<T>
        where T : IComparable<T>
    {
        public CalculationPriority(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public int CompareTo(ICalculationPriority<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var casted = obj as ICalculationPriority<T>;
            if (casted != null)
            {
                return CompareTo(casted);
            }

            throw new ArgumentException($"Object is not of type '{GetType()}'.");
        }
    }
}