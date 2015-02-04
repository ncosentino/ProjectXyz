using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStoreFactoryContract))]
    public interface IEnchantmentStoreFactory
    {
        #region Methods
        IEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid calculationId,
            Guid triggerId,
            Guid statusTypeId,
            TimeSpan remainingDuration,
            double value);
        #endregion
    }
}
