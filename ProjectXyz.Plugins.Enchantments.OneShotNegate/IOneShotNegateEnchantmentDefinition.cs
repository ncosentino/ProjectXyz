using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentDefinition : IEnchantmentDefinition
    {
        #region Properties
        Guid StatId { get; set; }
        #endregion
    }
}