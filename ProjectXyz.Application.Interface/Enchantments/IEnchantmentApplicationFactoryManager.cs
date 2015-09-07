using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentApplicationFactoryManager
    {
        #region Properties
        IEnchantmentFactory Enchantments { get; }

        IEnchantmentCalculatorResultFactory EnchantmentCalculatorResults { get; }

        IEnchantmentTypeCalculatorResultFactory EnchantmentTypeCalculatorResults { get; }
        #endregion
    }
}
