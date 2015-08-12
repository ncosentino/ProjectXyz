using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentGenerator
    {
        #region Methods
        IPercentageEnchantment Generate(
            IRandom randomizer,
            IPercentageEnchantmentDefinition definition);
        #endregion
    }
}
