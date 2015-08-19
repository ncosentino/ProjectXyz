using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentStore
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }
        #endregion
    }
}
