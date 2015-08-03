using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemContext))]
    public abstract class IItemContextContract : IItemContext
    {
        #region Properties
        public IStatSocketTypeRepository StatSocketTypeRepository
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatSocketTypeRepository>() != null);
                return default(IStatSocketTypeRepository);
            }
        }

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
