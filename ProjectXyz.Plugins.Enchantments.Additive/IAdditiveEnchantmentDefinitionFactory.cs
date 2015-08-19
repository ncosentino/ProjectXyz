using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentDefinitionFactory
    {
        #region Methods
        IAdditiveEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue);
        #endregion
    }
}
