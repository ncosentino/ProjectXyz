using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Enchantments;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginManager
    {
        #region Properties
        IPluginRepository<IEnchantmentPlugin> Enchantments { get; }
        #endregion
    }
}
