using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments
{
    public interface IEnchantmentApplier
    {
        IReadOnlyDictionary<IIdentifier, double> ApplyEnchantments(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats);
    }
}