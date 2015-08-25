using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentValueDefinitionFactory))]
    public abstract class IExpressionEnchantmentValueDefinitionFactoryContract : IExpressionEnchantmentValueDefinitionFactory
    {
        #region Methods
        public IExpressionEnchantmentValueDefinition CreateEnchantmentValueDefinition(
            Guid id, 
            Guid enchantmentDefinitionId,
            string idForExpression,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentOutOfRangeException>(maximumValue >= minimumValue);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinition>() != null);

            return default(IExpressionEnchantmentValueDefinition);
        }
        #endregion
    }
}
