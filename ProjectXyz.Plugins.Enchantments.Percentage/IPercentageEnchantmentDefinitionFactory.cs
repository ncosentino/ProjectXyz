using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Percentage.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    [ContractClass(typeof(IPercentageEnchantmentDefinitionFactoryContract))]
    public interface IPercentageEnchantmentDefinitionFactory
    {
        #region Methods
        IPercentageEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration);
        #endregion
    }
}
