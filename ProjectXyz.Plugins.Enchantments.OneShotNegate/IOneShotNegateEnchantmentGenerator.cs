using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentGenerator
    {
        #region Methods
        IOneShotNegateEnchantment Generate(
            IRandom randomizer,
            IEnchantmentDefinition enchantmentDefinition,
            IOneShotNegateEnchantmentDefinition oneShotNegateEnchantmentDefinition);
        #endregion
    }
}
