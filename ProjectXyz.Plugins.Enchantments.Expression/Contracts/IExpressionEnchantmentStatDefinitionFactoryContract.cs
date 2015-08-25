using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStatDefinitionFactory))]
    public abstract class IExpressionEnchantmentStatDefinitionFactoryContract : IExpressionEnchantmentStatDefinitionFactory
    {
        #region Methods
        public IExpressionEnchantmentStatDefinition CreateEnchantmentStatDefinition(
            Guid id, 
            Guid enchantmentDefinitionId,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinition>() != null);

            return default(IExpressionEnchantmentStatDefinition);
        }
        #endregion
    }
}
