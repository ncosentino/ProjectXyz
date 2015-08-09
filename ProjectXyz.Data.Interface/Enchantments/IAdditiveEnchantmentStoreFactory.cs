using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IAdditiveEnchantmentStoreFactoryContract))]
    public interface IAdditiveEnchantmentStoreFactory
    {
        #region Methods
        IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId,
            Guid statusTypeId,
            TimeSpan remainingDuration,
            double value);
        #endregion
    }
}
