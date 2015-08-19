using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentGenerator
    {
        #region Methods
        IAdditiveEnchantment Generate(
            IRandom randomizer,
            IEnchantmentDefinition enchantmentDefinition,
            IAdditiveEnchantmentDefinition additiveEnchantmentDefinition);
        #endregion
    }
}
