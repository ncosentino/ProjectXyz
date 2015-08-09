using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IAdditiveEnchantmentStoreFactory))]
    public abstract class IAdditiveEnchantmentStoreFactoryContract : IAdditiveEnchantmentStoreFactory
    {
        #region Methods
        public IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId,
            Guid statusTypeId,
            TimeSpan remainingDuration,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return default(IAdditiveEnchantmentStore);
        }
        #endregion
    }
}
