using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Plugins.Items;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginManager
    {
        #region Properties
        IEnumerable<IEnchantmentPlugin> Enchantments { get; }

        IEnumerable<IItemPlugin> Items { get; }
        #endregion
    }
}
