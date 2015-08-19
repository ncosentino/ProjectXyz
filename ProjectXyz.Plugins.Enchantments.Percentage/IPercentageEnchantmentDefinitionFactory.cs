using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentDefinitionFactory
    {
        #region Methods
        IPercentageEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue);
        #endregion
    }
}
