using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentFactory
    {
        #region Methods
        IPercentageEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            IEnumerable<Guid> weatherIds,
            TimeSpan remainingDuration,
            Guid statId,
            double value);
        #endregion
    }
}
