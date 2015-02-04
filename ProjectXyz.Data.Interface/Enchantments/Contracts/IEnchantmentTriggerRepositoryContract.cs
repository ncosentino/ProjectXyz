using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentTriggerRepository))]
    public abstract class IEnchantmentTriggerRepositoryContract : IEnchantmentTriggerRepository
    {
        #region Methods
        public IEnchantmentTrigger GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IEnchantmentTrigger>() != null);

            return default(IEnchantmentTrigger);
        }
        #endregion
    }
}
