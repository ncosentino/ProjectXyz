using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Core.Stats;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Worlds;
using ProjectXyz.Data.Interface;
using ProjectXyz.Game.Interface;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Core.Items;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Game.Core
{
    public sealed class GameManager : IGameManager
    {
        #region Constructors
        private GameManager(
            IDatabase database,
            IDataManager dataManager,
            IEnumerable<string> pluginDirectories)
        {
            DataManager = dataManager;

            var statApplicattionManager = StatApplicationManager.Create(dataManager);

            var enchantmentApplicationFactoryManager = EnchantmentApplicationFactoryManager.Create();

            var enchantmentPluginLoader = EnchantmentPluginLoader.Create(
                database,
                dataManager,
                enchantmentApplicationFactoryManager,
                pluginDirectories);

            var enchantmentTypeCalculators = enchantmentPluginLoader.Plugins.Select(x => x.EnchantmentTypeCalculator);

            var enchantmentApplicationManager = EnchantmentApplicationManager.Create(
                dataManager,
                enchantmentApplicationFactoryManager,
                enchantmentTypeCalculators);

            var itemApplicationManager = ItemApplicationManager.Create(
                dataManager,
                enchantmentApplicationManager);

            var itemPluginLoader = ItemPluginLoader.Create(
                database,
                dataManager,
                statApplicattionManager,
                itemApplicationManager,
                pluginDirectories);

            PluginManager = Plugins.Core.PluginManager.Create(
                enchantmentPluginLoader,
                itemPluginLoader);

            ApplicationManager = Application.Core.ApplicationManager.Create(
                DataManager,
                enchantmentApplicationFactoryManager,
                statApplicattionManager,
                itemApplicationManager);

            WorldManager = Application.Core.Worlds.WorldManager.Create();

            PluginRegistrarManager = Plugins.Core.PluginRegistrarManager.Create(
                DataManager,
                ApplicationManager);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IDataManager DataManager { get; }

        /// <inheritdoc />
        public IApplicationManager ApplicationManager { get; }

        /// <inheritdoc />
        public IWorldManager WorldManager { get; }

        /// <inheritdoc />
        public IPluginManager PluginManager { get; }

        /// <inheritdoc />
        public IPluginRegistrarManager PluginRegistrarManager { get; }
        #endregion

        #region Methods
        public static IGameManager Create(
            IDatabase database,
            IDataManager dataManager,
            IEnumerable<string> pluginDirectories)
        {
            var manager = new GameManager(
                database,
                dataManager,
                pluginDirectories);
            return manager;
        }
        #endregion
    }
}
