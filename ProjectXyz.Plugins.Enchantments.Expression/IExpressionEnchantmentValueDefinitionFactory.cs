using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentValueDefinitionFactoryContract))]
    public interface IExpressionEnchantmentValueDefinitionFactory
    {
        #region Methods
        IExpressionEnchantmentValueDefinition CreateEnchantmentValueDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            string idForExpression,
            double minimumValue,
            double maximumValue);
        #endregion
    }
}
