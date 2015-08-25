using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentDefinitionFactoryContract))]
    public interface IExpressionEnchantmentDefinitionFactory
    {
        #region Methods
        IExpressionEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            string expression,
            Guid statId,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration);
        #endregion
    }
}
