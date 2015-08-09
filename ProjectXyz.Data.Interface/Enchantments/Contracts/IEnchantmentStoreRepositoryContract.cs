using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStoreRepository<>))]
    public abstract class IEnchantmentStoreRepositoryContract<TEnchantmentStore> : IEnchantmentStoreRepository<TEnchantmentStore>
        where TEnchantmentStore : IEnchantmentStore
    {
        #region Methods
        public TEnchantmentStore GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<TEnchantmentStore>() != null);

            return default(TEnchantmentStore);
        }

        public void Add(TEnchantmentStore enchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStore != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
