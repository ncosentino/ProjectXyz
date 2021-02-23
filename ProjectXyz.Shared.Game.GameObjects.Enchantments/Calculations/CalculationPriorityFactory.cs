using System;

using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class CalculationPriorityFactory : ICalculationPriorityFactory
    {
        public ICalculationPriority<T> Create<T>(T value)
            where T : IComparable<T>
        {
            var calculationPriority = new CalculationPriority<T>(value);
            return calculationPriority;
        }
    }
}