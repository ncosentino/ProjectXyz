using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentStore
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }

        double Value { get; }

        TimeSpan RemainingDuration { get; }
        #endregion
    }
}
