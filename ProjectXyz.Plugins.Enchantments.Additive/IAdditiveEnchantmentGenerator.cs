using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentGenerator
    {
        #region Methods
        IAdditiveEnchantment Generate(
            IRandom randomizer,
            IAdditiveEnchantmentDefinition definition);
        #endregion
    }
}
