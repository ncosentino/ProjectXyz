using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentFactory
    {
        #region Methods
        IAdditiveEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId,
            double value);
        #endregion
    }
}
