using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Plugins.Enchantments.Additive;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IAdditiveEnchantmentStoreRepository))]
    public abstract class IAdditiveEnchantmentStoreRepositoryContract : IAdditiveEnchantmentStoreRepository
    {
        #region Methods
        public IAdditiveEnchantmentStore GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStore>() != null);

            return default(IAdditiveEnchantmentStore);
        }

        public void Add(IAdditiveEnchantmentStore enchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStore != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
