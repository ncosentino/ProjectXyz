using System;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface ICalculationPriority<T> :
        ICalculationPriority,
        IComparable<ICalculationPriority<T>>
        where T : IComparable<T>
    {
        T Value { get; }
    }

    public interface ICalculationPriority : IComparable
    {
    }
}