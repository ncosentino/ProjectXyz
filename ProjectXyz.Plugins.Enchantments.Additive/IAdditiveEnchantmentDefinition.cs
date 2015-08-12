using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentDefinition : IEnchantmentDefinition
    {
        #region Properties
        Guid StatId { get; set; }

        double MinimumValue { get; set; }

        double MaximumValue { get; set; }
        #endregion
    }
}