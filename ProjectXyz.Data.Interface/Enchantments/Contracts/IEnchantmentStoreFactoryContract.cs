using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStoreFactory))]
    public abstract class IEnchantmentStoreFactoryContract : IEnchantmentStoreFactory
    {
        #region Methods
        public IEnchantmentStore CreateEnchantmentStore(
            Guid statId, 
            Guid calculationId,
            Guid triggerId,
            Guid statusTypeId,
            TimeSpan remainingDuration,
            double value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return default(IEnchantmentStore);
        }
        #endregion
    }
}
