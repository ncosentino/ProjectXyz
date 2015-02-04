using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStoreRepository))]
    public abstract class IEnchantmentStoreRepositoryContract : IEnchantmentStoreRepository
    {
        #region Methods
        public IEnchantmentStore GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return default(IEnchantmentStore);
        }

        public void Add(IEnchantmentStore enchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStore != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
