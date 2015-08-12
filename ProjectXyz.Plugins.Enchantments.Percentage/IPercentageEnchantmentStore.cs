using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentStore : IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }
}
