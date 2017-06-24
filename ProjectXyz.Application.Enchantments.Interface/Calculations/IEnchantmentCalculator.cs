using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Interface.Calculations
{
    public interface IEnchantmentCalculator
    {
        double Calculate(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}