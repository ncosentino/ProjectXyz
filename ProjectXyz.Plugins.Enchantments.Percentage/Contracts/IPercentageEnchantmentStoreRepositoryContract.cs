using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Plugins.Enchantments.Percentage;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IPercentageEnchantmentStoreRepository))]
    public abstract class IPercentageEnchantmentStoreRepositoryContract : IPercentageEnchantmentStoreRepository
    {
        #region Methods
        public IPercentageEnchantmentStore GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IPercentageEnchantmentStore>() != null);

            return default(IPercentageEnchantmentStore);
        }

        public void Add(IPercentageEnchantmentStore enchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStore != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
