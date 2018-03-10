using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Enchantments.Interface.Calculations
{
    public interface IEnchantmentStatCalculator
    {
        double Calculate(
            IReadOnlyCollection<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}