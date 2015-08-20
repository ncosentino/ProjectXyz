using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Percentage.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    [ContractClass(typeof(IPercentageEnchantmentStoreFactoryContract))]
    public interface IPercentageEnchantmentStoreFactory
    {
        #region Methods
        IPercentageEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration);
        #endregion
    }
}
