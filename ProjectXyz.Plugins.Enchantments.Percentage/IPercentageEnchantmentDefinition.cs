using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public interface IPercentageEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid StatId { get; set; }

        double MinimumValue { get; set; }

        double MaximumValue { get; set; }

        TimeSpan MinimumDuration { get; set; }

        TimeSpan MaximumDuration { get; set; }
        #endregion
    }
}