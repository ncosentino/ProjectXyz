using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Core;
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
            throw new NotImplementedException();
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
            
            var pluginProvider = new PluginProvider(new IPluginRepository[]
            {
                
            });
            return pluginProvider;
        }
    }
}
