using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemContext))]
    public abstract class IItemContextContract : IItemContext
    {
        #region Properties
        public IEnchantmentCalculator EnchantmentCalculator
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentCalculator>() != null);
                return default(IEnchantmentCalculator);
            }
        }

        public IEnchantmentContext EnchantmentContext
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentContext>() != null);
                return default(IEnchantmentContext);
            }
        }
        #endregion
    }
}
