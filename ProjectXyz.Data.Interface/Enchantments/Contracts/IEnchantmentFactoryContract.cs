using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentFactory))]
    public abstract class IEnchantmentFactoryContract : IEnchantmentFactory
    {
        #region Methods
        public IEnchantment CreateEnchantment()
        {
            Contract.Ensures(Contract.Result<IEnchantment>() != null);

            return default(IEnchantment);
        }
        #endregion
    }
}
