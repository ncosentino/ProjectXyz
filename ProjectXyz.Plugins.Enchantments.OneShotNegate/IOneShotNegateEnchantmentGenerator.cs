using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentGenerator
    {
        #region Methods
        IOneShotNegateEnchantment Generate(
            IRandom randomizer,
            IOneShotNegateEnchantmentDefinition definition);
        #endregion
    }
}
