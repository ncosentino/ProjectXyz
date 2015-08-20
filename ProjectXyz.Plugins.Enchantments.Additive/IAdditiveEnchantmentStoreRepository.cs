using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Additive.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    [ContractClass(typeof(IAdditiveEnchantmentStoreRepositoryContract))]
    public interface IAdditiveEnchantmentStoreRepository
    {
        #region Methods
        void Add(IAdditiveEnchantmentStore enchantmentStore);

        void RemoveById(Guid id);

        IAdditiveEnchantmentStore GetById(Guid id);
        #endregion
    }
}
