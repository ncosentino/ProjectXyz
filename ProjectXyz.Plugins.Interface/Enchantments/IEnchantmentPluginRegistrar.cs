using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Enchantments;

namespace ProjectXyz.Plugins.Interface.Enchantments
{
    public interface IEnchantmentPluginRegistrar
    {
        #region Methods
        void RegisterPlugins(IEnumerable<IEnchantmentPlugin> enchantmentPlugins);
        #endregion
    }
}
