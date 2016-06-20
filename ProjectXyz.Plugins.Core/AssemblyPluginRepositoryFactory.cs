using System;
using System.Collections.Generic;
using System.Reflection;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public sealed class AssemblyPluginRepositoryFactory
    {
        private readonly ITry _try;
        private readonly Func<string, IEnumerable<string>> _getPluginCandidateFilesCallback;
        private readonly Func<string, Assembly> _createAssemblyCallback;
        private readonly IReadOnlyCollection<string> _pluginDirectories;

        public AssemblyPluginRepositoryFactory(
            ITry @try,
            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback,
            Func<string, Assembly> createAssemblyCallback,
            IReadOnlyCollection<string> pluginDirectories)
        {
            _try = @try;
            _getPluginCandidateFilesCallback = getPluginCandidateFilesCallback;
            _createAssemblyCallback = createAssemblyCallback;
            _pluginDirectories = pluginDirectories;
        }

        public IPluginRepository<TPlugin> Create<TPlugin>(Func<Assembly, Type, TPlugin> createPluginCallback)
            where TPlugin : IPlugin
        {
            return new PluginRepository<TPlugin>(
                _try,
                _getPluginCandidateFilesCallback,
                _createAssemblyCallback,
                createPluginCallback,
                _pluginDirectories);
        }
    }
}
