using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Enchantments.Interface.Calculations
{
    public interface IEnchantmentApplier
    {
        IReadOnlyDictionary<IIdentifier, double> ApplyEnchantments(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats);
    }
}