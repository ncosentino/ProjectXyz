using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAdditiveEnchantmentDefinitionFactory
    {
        #region Methods
        IAdditiveEnchantmentDefinition CreateEnchantmentDefinition(
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
