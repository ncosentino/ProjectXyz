using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAdditiveEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid StatId { get; set; }

        Guid TriggerId { get; set; }

        Guid StatusTypeId { get; set; }

        TimeSpan MinimumDuration { get; set; }
        
        TimeSpan MaximumDuration { get; set; }

        double MinimumValue { get; set; }

        double MaximumValue { get; set; }
        #endregion
    }
}