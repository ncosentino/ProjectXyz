using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculator
    {
        double Calculate(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}