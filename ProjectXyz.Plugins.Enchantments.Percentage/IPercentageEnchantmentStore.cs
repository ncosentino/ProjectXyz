using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentStore
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }
}
