using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IOneShotNegateEnchantmentStoreRepository))]
    public abstract class IOneShotNegateEnchantmentStoreRepositoryContract : IOneShotNegateEnchantmentStoreRepository
    {
        #region Methods
        public IOneShotNegateEnchantmentStore GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStore>() != null);

            return default(IOneShotNegateEnchantmentStore);
        }

        public void Add(IOneShotNegateEnchantmentStore enchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStore != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
