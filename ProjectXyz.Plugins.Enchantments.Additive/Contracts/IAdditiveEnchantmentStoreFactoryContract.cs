using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive.Contracts
{
    [ContractClassFor(typeof(IAdditiveEnchantmentStoreFactory))]
    public abstract class IAdditiveEnchantmentStoreFactoryContract : IAdditiveEnchantmentStoreFactory
    {
        #region Methods
        public IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStore>() != null);

            return default(IAdditiveEnchantmentStore);
        }
        #endregion
    }
}
