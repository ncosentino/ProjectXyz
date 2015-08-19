using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid StatId { get; set; }
        #endregion
    }
}