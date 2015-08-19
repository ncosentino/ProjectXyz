using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentGenerator
    {
        #region Methods
        IPercentageEnchantment Generate(
            IRandom randomizer,
            IEnchantmentDefinition enchantmentDefinition,
            IPercentageEnchantmentDefinition percentageEnchantmentDefinition);
        #endregion
    }
}
