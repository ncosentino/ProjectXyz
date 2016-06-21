using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ProjectXyz.Application.Core.Weather;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Core;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Game.Core.Plugins
{
    public sealed class AssemblyPluginProviderFactory
    {
        private readonly ITry _try;
        private readonly string _fileSearchPattern;
        private readonly bool _includeChildDirectories;
        private readonly IReadOnlyCollection<string> _pluginDirectories;

        public AssemblyPluginProviderFactory(
            ITry @try,
            string fileSearchPattern,
            bool includeChildDirectories,
            params string[] pluginDirectories)
        {
            _try = @try;
            _fileSearchPattern = fileSearchPattern;
            _includeChildDirectories = includeChildDirectories;
            _pluginDirectories = pluginDirectories;
        }

        public IPluginProvider Create()
        {
            var assemblyPluginRepositoryFactory = new AssemblyPluginRepositoryFactory(
                _try,
                x => Directory.GetFiles(
                    x,
                    _fileSearchPattern,
                    _includeChildDirectories
                        ? SearchOption.AllDirectories
                        : SearchOption.TopDirectoryOnly),
                Assembly.LoadFrom,
                _pluginDirectories);

            var weatherManager = new WeatherManager();
            var enchantmentPluginInitializationProvider = new EnchantmentPluginInitializationProvider(weatherManager);
            var enchantmentPluginLoader = new EnchantmentPluginAssemblyLoader(enchantmentPluginInitializationProvider);

            var pluginRepositories = new[]
            {
                assemblyPluginRepositoryFactory.Create(enchantmentPluginLoader.LoadFromAssembly),
            };

            var pluginProvider = new PluginProvider(pluginRepositories);
            return pluginProvider;
        }
    }
}
