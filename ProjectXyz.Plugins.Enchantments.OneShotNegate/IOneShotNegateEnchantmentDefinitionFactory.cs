using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentDefinitionFactory
    {
        #region Methods
        IOneShotNegateEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId);
        #endregion
    }
}
