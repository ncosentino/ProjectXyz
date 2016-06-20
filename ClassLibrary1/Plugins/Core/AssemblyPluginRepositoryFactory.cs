using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Framework.Interface;
using ClassLibrary1.Plugins.Api;
using ClassLibrary1.Plugins.Interface;

namespace ClassLibrary1.Plugins.Core
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
