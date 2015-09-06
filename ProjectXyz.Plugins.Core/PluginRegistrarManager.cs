using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Interface;
using ProjectXyz.Plugins.Interface.Enchantments;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginRegistrarManager : IPluginRegistrarManager
    {
        #region Fields
        private readonly IEnchantmentPluginRegistrar _enchantmentPluginRegistrar;
        #endregion

        #region Constructors
        private PluginRegistrarManager(
            IDataManager dataManager,
            IApplicationManager applicationManager)
        {
            _enchantmentPluginRegistrar = EnchantmentPluginRegistrar.Create(
                applicationManager.Enchantments.EnchantmentFactory,
                applicationManager.Enchantments.EnchantmentSaver,
                applicationManager.Enchantments.EnchantmentGenerator);
        }
        #endregion

        #region Properties
        public IEnchantmentPluginRegistrar Enchantments { get { return _enchantmentPluginRegistrar; } }
        #endregion

        #region Methods
        public static IPluginRegistrarManager Create(
            IDataManager dataManager,
            IApplicationManager applicationManager)
        {
            var manager = new PluginRegistrarManager(
                dataManager,
                applicationManager);
            return manager;
        }
        #endregion
    }
}
