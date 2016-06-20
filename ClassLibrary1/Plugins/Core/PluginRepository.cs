using System;
using System.Collections.Generic;
using System.Reflection;
using ClassLibrary1.Framework.Interface;
using ClassLibrary1.Plugins.Api;
using ClassLibrary1.Plugins.Interface;

namespace ClassLibrary1.Plugins.Core
{
    public sealed class PluginRepository<TPlugin> : 
        IPluginRepository<TPlugin>
        where TPlugin : IPlugin
    {
        #region Fields
        private readonly ITry _try;
        private readonly Func<string, IEnumerable<string>> _getPluginCandidateFilesCallback;
        private readonly Func<string, Assembly> _createAssemblyCallback;
        private readonly Func<Assembly, Type, TPlugin> _createPluginCallback;
        private readonly List<TPlugin> _plugins;
        private readonly IReadOnlyCollection<string> _pluginDirectoryPaths;
        #endregion

        #region Constructors
        public PluginRepository(
            ITry @try,
            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback,
            Func<string, Assembly> createAssemblyCallback,
            Func<Assembly, Type, TPlugin> createPluginCallback,
            IReadOnlyCollection<string> pluginDirectoryPaths)
        {
            _try = @try;
            _getPluginCandidateFilesCallback = getPluginCandidateFilesCallback;
            _createAssemblyCallback = createAssemblyCallback;
            _createPluginCallback = createPluginCallback;
            _pluginDirectoryPaths = pluginDirectoryPaths;
            _plugins = new List<TPlugin>();
        }
        #endregion

        #region Properties
        public IReadOnlyCollection<TPlugin> Plugins
        {
            get
            {
                lock (_plugins)
                {
                    if (_plugins.Count < 1)
                    {
                        _plugins.AddRange(FindPlugins(_pluginDirectoryPaths));
                    }
                }

                return _plugins;
            }
        }
        #endregion

        #region Methods
        private IEnumerable<TPlugin> FindPlugins(IEnumerable<string> pluginDirectoryPaths)
        {
            foreach (var pluginDirectoryPath in pluginDirectoryPaths)
            {
                foreach (var potentialPluginFile in _getPluginCandidateFilesCallback.Invoke(pluginDirectoryPath))
                {
                    Assembly potentialPluginAssembly;
                    try
                    {
                        potentialPluginAssembly = _createAssemblyCallback.Invoke(potentialPluginFile);
                    }
                    catch (BadImageFormatException)
                    {
                        continue;
                    }

                    foreach (var potentialPluginType in potentialPluginAssembly.GetExportedTypes())
                    {
                        if (potentialPluginType.IsAbstract ||
                            potentialPluginType.IsInterface ||
                            !typeof(TPlugin).IsAssignableFrom(potentialPluginType))
                        {
                            continue;
                        }

                        TPlugin pluginInstance = default(TPlugin);
                        var exception = _try.Dangerous(() =>
                        {
                            try
                            {
                                pluginInstance = _createPluginCallback.Invoke(
                                    potentialPluginAssembly,
                                    potentialPluginType);
                            }
                            catch (Exception ex)
                            {
                                throw new InvalidOperationException(
                                    $"Could not create plugin instance '{potentialPluginType}' from '{potentialPluginFile}'.",
                                    ex);
                            }
                        });
                        if (exception != null)
                        {
                            continue;
                        }
                        
                        yield return pluginInstance;
                    }
                }
            }
        }
        #endregion
    }
}
