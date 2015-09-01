
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Items;

namespace ProjectXyz.Plugins.Interface.Items
{
    public interface IItemPluginRegistrar
    {
        #region Methods
        void RegisterPlugins(IEnumerable<IItemPlugin> enchantmentPlugins);
        #endregion
    }
}
