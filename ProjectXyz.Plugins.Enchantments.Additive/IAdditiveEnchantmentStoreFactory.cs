using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Additive.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    [ContractClass(typeof(IAdditiveEnchantmentStoreFactoryContract))]
    public interface IAdditiveEnchantmentStoreFactory
    {
        #region Methods
        IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration);
        #endregion
    }
}
