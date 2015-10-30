using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface;
using ProjectXyz.Plugins.Interface;
using ProjectXyz.Plugins.Items;

namespace ProjectXyz.Plugins.Core.Items
{
    public sealed class ItemPluginLoader :
        PluginLoader<IItemPlugin>,
        IPluginRepository<IItemPlugin>
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDataManager _dataManager;
        private readonly IStatApplicationManager _statApplicationManager;
        private readonly IItemApplicationManager _itemApplicationManager;
        #endregion

        #region Constructors
        private ItemPluginLoader(
            IDatabase database,
            IDataManager dataManager,
            IStatApplicationManager statApplicationManager,
            IItemApplicationManager itemApplicationManager,
            IEnumerable<string> pluginDirectoryPaths)
            : base(pluginDirectoryPaths)
        {
            _database = database;
            _dataManager = dataManager;
            _statApplicationManager = statApplicationManager;
            _itemApplicationManager = itemApplicationManager;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnumerable<IItemPlugin> Plugins
        {
            get { return PluginRepository.Plugins; }
        }

        /// <inheritdoc />
        public void Scan()
        {
            PluginRepository.Scan();
        }
        #endregion

        #region Methods
        public static IPluginRepository<IItemPlugin> Create(
            IDatabase database,
            IDataManager dataManager,
            IStatApplicationManager statApplicationManager,
            IItemApplicationManager itemApplicationManager,
            IEnumerable<string> pluginDirectoryPaths)
        {
            var loader = new ItemPluginLoader(
                database,
                dataManager,
                statApplicationManager,
                itemApplicationManager,
                pluginDirectoryPaths);
            return loader;
        }

        /// <inheritdoc />
        protected override object[] CreatePluginConstructorArguments()
        {
            var arguments = new object[]
            {
                _database,
                _dataManager,
                _statApplicationManager,
                _itemApplicationManager,
            };
            return arguments;
        }

        /// <inheritdoc />
        protected override Type[] CreatePluginConstructorTypes()
        {
            var types = new[]
            {
                typeof(IDatabase),
                typeof(IDataManager),
                typeof(IStatApplicationManager),
                typeof(IItemApplicationManager),
            };
            return types;
        }
        #endregion
    }
}
