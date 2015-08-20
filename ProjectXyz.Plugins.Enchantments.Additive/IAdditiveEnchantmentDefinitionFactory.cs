using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Additive.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    [ContractClass(typeof(IAdditiveEnchantmentDefinitionFactoryContract))]
    public interface IAdditiveEnchantmentDefinitionFactory
    {
        #region Methods
        IAdditiveEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration);
        #endregion
    }
}
