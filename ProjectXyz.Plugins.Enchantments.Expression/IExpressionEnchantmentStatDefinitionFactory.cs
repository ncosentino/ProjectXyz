using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStatDefinitionFactoryContract))]
    public interface IExpressionEnchantmentStatDefinitionFactory
    {
        #region Methods
        IExpressionEnchantmentStatDefinition CreateEnchantmentStatDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            string idForExpression,
            Guid statId);
        #endregion
    }
}
