using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentDefinitionFactory))]
    public abstract class IExpressionEnchantmentDefinitionFactoryContract : IExpressionEnchantmentDefinitionFactory
    {
        #region Methods
        public IExpressionEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid expressionId,
            Guid statId,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= minimumDuration);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinition>() != null);

            return default(IExpressionEnchantmentDefinition);
        }
        #endregion
    }
}
