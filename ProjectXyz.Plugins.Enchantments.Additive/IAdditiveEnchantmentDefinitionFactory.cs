using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentDefinitionFactory
    {
        #region Methods
        IEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            Guid triggerId,
            Guid statusTypeId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration);
        #endregion
    }
}
