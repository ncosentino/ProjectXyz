using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantment : IEnchantment
    {
        #region Properties
        Guid StatId { get; }
        #endregion
    }
}
