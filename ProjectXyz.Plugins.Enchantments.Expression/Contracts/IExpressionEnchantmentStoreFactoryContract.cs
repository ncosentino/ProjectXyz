using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStoreFactory))]
    public abstract class IExpressionEnchantmentStoreFactoryContract : IExpressionEnchantmentStoreFactory
    {
        #region Methods
        public IExpressionEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            string expression,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(expression));
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStore>() != null);

            return default(IExpressionEnchantmentStore);
        }
        #endregion
    }
}
