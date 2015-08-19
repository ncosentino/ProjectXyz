using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    [ContractClass(typeof(IPercentageEnchantmentStoreRepositoryContract))]
    public interface IPercentageEnchantmentStoreRepository
    {
        #region Methods
        void Add(IPercentageEnchantmentStore enchantmentStore);

        void RemoveById(Guid id);

        IPercentageEnchantmentStore GetById(Guid id);
        #endregion
    }
}
