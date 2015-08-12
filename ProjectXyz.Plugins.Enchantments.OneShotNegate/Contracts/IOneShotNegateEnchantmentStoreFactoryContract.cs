using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Contracts
{
    [ContractClassFor(typeof(IOneShotNegateEnchantmentStoreFactory))]
    public abstract class IOneShotNegateEnchantmentStoreFactoryContract : IOneShotNegateEnchantmentStoreFactory
    {
        #region Methods
        public IOneShotNegateEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return default(IOneShotNegateEnchantmentStore);
        }
        #endregion
    }
}
