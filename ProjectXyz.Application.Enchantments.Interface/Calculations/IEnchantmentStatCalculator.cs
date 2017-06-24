using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

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