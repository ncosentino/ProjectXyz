using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentApplicationManager
    {
        #region Properties
        IEnchantmentGenerator EnchantmentGenerator { get; }

        IEnchantmentCalculator EnchantmentCalculator { get; }

        IEnchantmentSaver EnchantmentSaver { get; }

        IEnchantmentApplicationFactoryManager Factories { get; }
        #endregion
    }
}
