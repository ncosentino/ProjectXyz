using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    [ContractClass(typeof(IOneShotNegateEnchantmentStoreRepositoryContract))]
    public interface IOneShotNegateEnchantmentStoreRepository
    {
        #region Methods
        void Add(IOneShotNegateEnchantmentStore enchantmentStore);

        void RemoveById(Guid id);

        IOneShotNegateEnchantmentStore GetById(Guid id);
        #endregion
    }
}
