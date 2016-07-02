using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentStatCalculator
    {
        double Calculate(
            IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IIdentifier statDefinitionId);
    }
}