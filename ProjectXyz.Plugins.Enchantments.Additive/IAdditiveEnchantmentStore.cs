using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentStore
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }

        double Value { get; }

        TimeSpan RemainingDuration { get; }
        #endregion
    }
}
