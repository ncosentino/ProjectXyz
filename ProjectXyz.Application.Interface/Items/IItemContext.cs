using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemContextContract))]
    public interface IItemContext
    {
        #region Properties
        IEnchantmentCalculator EnchantmentCalculator { get; }

        IEnchantmentContext EnchantmentContext { get; }
        #endregion
    }
}
