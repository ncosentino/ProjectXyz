using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStoreRepositoryContract<>))]
    public interface IEnchantmentStoreRepository<TEnchantmentStore>
        where TEnchantmentStore : IEnchantmentStore
    {
        #region Methods
        void Add(TEnchantmentStore enchantmentStore);

        void RemoveById(Guid id);

        TEnchantmentStore GetById(Guid id);
        #endregion
    }
}
