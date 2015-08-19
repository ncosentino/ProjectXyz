using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.OneShotNegate.Contracts;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    [ContractClass(typeof(IOneShotNegateEnchantmentStoreFactoryContract))]
    public interface IOneShotNegateEnchantmentStoreFactory
    {
        #region Methods
        IOneShotNegateEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId);
        #endregion
    }
}
