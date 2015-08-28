using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }
        #endregion
    }
}