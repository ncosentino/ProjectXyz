using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive.Contracts
{
    [ContractClassFor(typeof(IAdditiveEnchantmentStoreFactory))]
    public abstract class IAdditiveEnchantmentStoreFactoryContract : IAdditiveEnchantmentStoreFactory
    {
        #region Methods
        public IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStore>() != null);

            return default(IAdditiveEnchantmentStore);
        }
        #endregion
    }
}
