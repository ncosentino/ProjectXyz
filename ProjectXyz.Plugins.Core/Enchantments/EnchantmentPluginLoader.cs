using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core.Enchantments
{
    public sealed class EnchantmentPluginLoader : 
        PluginLoader<IEnchantmentPlugin>,
        IPluginRepository<IEnchantmentPlugin>
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDataManager _dataManager;
        private readonly IEnchantmentApplicationFactoryManager _enchantmentApplicationFactoryManager;
        #endregion

        #region Constructors
        private EnchantmentPluginLoader(
            IDatabase database,
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IEnumerable<string> pluginDirectoryPaths)
            : base(pluginDirectoryPaths)
        {
            _database = database;
            _dataManager = dataManager;
            _enchantmentApplicationFactoryManager = enchantmentApplicationFactoryManager;
        }
        #endregion

        #region Properties        
        /// <inheritdoc />
        public IEnumerable<IEnchantmentPlugin> Plugins
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
        public static IPluginRepository<IEnchantmentPlugin> Create(
            IDatabase database,
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IEnumerable<string> pluginDirectoryPaths)
        {
            var loader = new EnchantmentPluginLoader(
                database,
                dataManager,
                enchantmentApplicationFactoryManager,
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
                _enchantmentApplicationFactoryManager,
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
                typeof(IEnchantmentApplicationFactoryManager),
            };
            return types;
        }
        #endregion
    }
}
