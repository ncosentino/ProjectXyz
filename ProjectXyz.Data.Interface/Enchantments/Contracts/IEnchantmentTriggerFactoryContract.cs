using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentTriggerFactory))]
    public abstract class IEnchantmentTriggerFactoryContract : IEnchantmentTriggerFactory
    {
        #region Methods
        public IEnchantmentTrigger CreateEnchantmentTrigger(Guid id, string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentTrigger>() != null);

            return default(IEnchantmentTrigger);
        }
        #endregion
    }
}
