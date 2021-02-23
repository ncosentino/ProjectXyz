using System;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface ICalculationPriorityFactory
    {
        ICalculationPriority<T> Create<T>(T value) where T : IComparable<T>;
    }
}