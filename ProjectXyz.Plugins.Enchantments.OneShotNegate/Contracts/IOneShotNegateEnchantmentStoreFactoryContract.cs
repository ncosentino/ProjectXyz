using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Contracts
{
    [ContractClassFor(typeof(IOneShotNegateEnchantmentStoreFactory))]
    public abstract class IOneShotNegateEnchantmentStoreFactoryContract : IOneShotNegateEnchantmentStoreFactory
    {
        #region Methods
        public IOneShotNegateEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStore>() != null);

            return default(IOneShotNegateEnchantmentStore);
        }
        #endregion
    }
}
