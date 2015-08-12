using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantment : IEnchantment
    {
        #region Properties
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }
}
