using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentStore : IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }
        #endregion
    }
}
