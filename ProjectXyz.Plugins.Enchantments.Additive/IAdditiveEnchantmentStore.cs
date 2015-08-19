using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public interface IAdditiveEnchantmentStore
    {
        #region Properties
        Guid Id { get; }
        
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }
}
