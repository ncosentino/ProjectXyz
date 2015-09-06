using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginManager : IPluginManager
    {
        #region Fields
        private readonly IPluginRepository<IEnchantmentPlugin> _enchantmentPluginRepository;
        private readonly IDatabase _database;
        private readonly IDataManager _dataManager;
        private readonly IApplicationManager _applicationManager;
        #endregion

        #region Constructors
        private PluginManager(
            IDatabase database,
            IDataManager dataManager,
            IApplicationManager applicationManager,
            IEnumerable<string> pluginDirectoryPaths)
        {
            _database = database;
            _dataManager = dataManager;
            _applicationManager = applicationManager;

            _enchantmentPluginRepository = PluginRepository<IEnchantmentPlugin>.Create(
                GetPluginCandidateFiles,
                CreateAssembly,
                CreateEnchantmentPlugin,
                pluginDirectoryPaths);
        }
        #endregion

        #region Properties
        public IPluginRepository<IEnchantmentPlugin> Enchantments { get { return _enchantmentPluginRepository; } }
        #endregion

        #region Methods
        public static IPluginManager Create(
            IDatabase database,
            IDataManager dataManager,
            IApplicationManager applicationManager,
            IEnumerable<string> pluginDirectoryPaths)
        {
            var manager = new PluginManager(
                database,
                dataManager,
                applicationManager,
                pluginDirectoryPaths);
            return manager;
        }

        private IEnumerable<string> GetPluginCandidateFiles(string directory)
        {
            return Directory.GetFiles("*plugin*.dll");
        }

        private Assembly CreateAssembly(string pathToAssembly)
        {
            var assembly = Assembly.LoadFile(pathToAssembly);
            return assembly;
        }

        private IEnchantmentPlugin CreateEnchantmentPlugin(Assembly assembly, Type type)
        {
            var constructor = type.GetConstructor(
                BindingFlags.CreateInstance |
                BindingFlags.NonPublic |
                BindingFlags.Public,
                null,
                new[]
                {
                    typeof(IDatabase),
                    typeof(IDataManager),
                    typeof(IApplicationManager),
                },
                null);
            if (constructor == null)
            {
                throw new InvalidOperationException(string.Format("The plugin '{0}' does not have a constructor with the correct paramaters.", type.FullName));
            }

            var instance = (IEnchantmentPlugin)constructor.Invoke(
                null,
                new object[]
                {
                    _database,
                    _dataManager,
                    _applicationManager,
                });

            return instance;
        }
        #endregion
    }
}
