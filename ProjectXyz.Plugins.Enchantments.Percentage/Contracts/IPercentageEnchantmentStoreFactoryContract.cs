using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Contracts
{
    [ContractClassFor(typeof(IPercentageEnchantmentStoreFactory))]
    public abstract class IPercentageEnchantmentStoreFactoryContract : IPercentageEnchantmentStoreFactory
    {
        #region Methods
        public IPercentageEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentStore>() != null);

            return default(IPercentageEnchantmentStore);
        }
        #endregion
    }
}
