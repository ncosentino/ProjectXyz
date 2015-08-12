using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjectXyz.Plugins
{
    public sealed class PluginRepository<TPlugin> : IPluginRepository<TPlugin>
    {
        #region Fields
        private readonly Func<string, IEnumerable<string>> _getPluginCandidateFilesCallback;
        private readonly Func<string, Assembly> _createAssemblyCallback;
        private readonly Func<Assembly, Type, TPlugin> _createPluginCallback;
        private readonly List<TPlugin> _plugins;
        private readonly List<string> _pluginDirectoryPaths;
        #endregion

        #region Constructors
        private PluginRepository(
            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback,
            Func<string, Assembly> createAssemblyCallback,
            Func<Assembly, Type, TPlugin> createPluginCallback,
            IEnumerable<string> pluginDirectoryPaths)
        {
            _getPluginCandidateFilesCallback = getPluginCandidateFilesCallback;
            _createAssemblyCallback = createAssemblyCallback;
            _createPluginCallback = createPluginCallback;
            _pluginDirectoryPaths = new List<string>(pluginDirectoryPaths);
            _plugins = new List<TPlugin>();
        }
        #endregion

        #region Properties
        public IEnumerable<TPlugin> Plugins
        {
            get
            {
                lock (_plugins)
                {
                    if (_plugins.Count < 1)
                    {
                        Scan();
                    }
                }

                return _plugins;
            }
        }
        #endregion

        #region Methods
        public static IPluginRepository<TPlugin> Create(
            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback,
            Func<string, Assembly> createAssemblyCallback,
            Func<Assembly, Type, TPlugin> createPluginCallback,
            params string[] pluginDirectoryPaths)
        {
            var pluginRepsoitory = Create(
                getPluginCandidateFilesCallback,
                createAssemblyCallback,
                createPluginCallback,
                (IEnumerable<string>)pluginDirectoryPaths);
            return pluginRepsoitory;
        }

        public static IPluginRepository<TPlugin> Create(
            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback,
            Func<string, Assembly> createAssemblyCallback,
            Func<Assembly, Type, TPlugin> createPluginCallback,
            IEnumerable<string> pluginDirectoryPaths)
        {
            var pluginRepsoitory = new PluginRepository<TPlugin>(
                getPluginCandidateFilesCallback,
                createAssemblyCallback,
                createPluginCallback,
                pluginDirectoryPaths);
            return pluginRepsoitory;
        }

        public void Scan()
        {
            _plugins.Clear();
            _plugins.AddRange(FindPlugins(_pluginDirectoryPaths));
        }

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

                        TPlugin pluginInstance;
                        try
                        {
                            pluginInstance = _createPluginCallback.Invoke(potentialPluginAssembly, potentialPluginType);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(string.Format(
                                "Could not create plugin instance '{0}' from '{1}'.", 
                                potentialPluginType, 
                                potentialPluginFile), ex);
                        }

                        yield return pluginInstance;
                    }
                }
            }
        }
        #endregion
    }
}
