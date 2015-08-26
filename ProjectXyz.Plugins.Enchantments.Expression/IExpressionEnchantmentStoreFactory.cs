using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStoreFactoryContract))]
    public interface IExpressionEnchantmentStoreFactory
    {
        #region Methods
        IExpressionEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            Guid expressionId,
            TimeSpan remainingDuration);
        #endregion
    }
}
