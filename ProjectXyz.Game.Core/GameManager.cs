using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Core.Stats;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface;
using ProjectXyz.Game.Interface;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Core.Items;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Game.Core
{
    public sealed class GameManager : IGameManager
    {
        #region Fields
        private readonly IDataManager _dataManager;
        private readonly IApplicationManager _applicationManager;
        private readonly IPluginManager _pluginManager;
        private readonly IPluginRegistrarManager _pluginRegistrarManager;
        #endregion

        #region Constructors
        private GameManager(
            IDatabase database,
            IDataManager dataManager,
            IEnumerable<string> pluginDirectories)
        {
            _dataManager = dataManager;

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

            _pluginManager = Plugins.Core.PluginManager.Create(
                enchantmentPluginLoader,
                itemPluginLoader);

            _applicationManager = Application.Core.ApplicationManager.Create(
                _dataManager,
                enchantmentApplicationFactoryManager,
                statApplicattionManager,
                itemApplicationManager);

            _pluginRegistrarManager = Plugins.Core.PluginRegistrarManager.Create(
                _dataManager,
                _applicationManager);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IDataManager DataManager { get { return _dataManager; } }

        /// <inheritdoc />
        public IApplicationManager ApplicationManager { get { return _applicationManager; } }

        /// <inheritdoc />
        public IPluginManager PluginManager { get { return _pluginManager; } }

        /// <inheritdoc />
        public IPluginRegistrarManager PluginRegistrarManager { get { return _pluginRegistrarManager; } }
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
