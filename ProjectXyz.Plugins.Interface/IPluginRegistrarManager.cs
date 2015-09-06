using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Interface.Enchantments;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginRegistrarManager
    {
        #region Properties
        IEnchantmentPluginRegistrar Enchantments { get; }
        #endregion
    }
}
