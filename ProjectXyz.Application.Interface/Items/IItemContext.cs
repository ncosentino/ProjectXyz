using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemContextContract))]
    public interface IItemContext
    {
        #region Properties
        IStatSocketTypeRepository StatSocketTypeRepository { get; }

        IEnchantmentCalculator EnchantmentCalculator { get; }

        IEnchantmentContext EnchantmentContext { get; }
        #endregion
    }
}
