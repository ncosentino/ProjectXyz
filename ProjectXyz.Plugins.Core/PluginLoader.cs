using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public abstract class PluginLoader<TPlugin>
    {
        #region Fields
        private readonly IPluginRepository<TPlugin> _pluginRepository;
        #endregion

        #region Constructors
        protected PluginLoader(IEnumerable<string> pluginDirectoryPaths)
        {
            _pluginRepository = PluginRepository<TPlugin>.Create(
                GetPluginCandidateFiles,
                CreateAssembly,
                CreatePlugin,
                pluginDirectoryPaths);
        }
        #endregion

        #region Properties
        protected IPluginRepository<TPlugin>  PluginRepository { get { return _pluginRepository; } }
        #endregion

        #region Methods
        protected abstract Type[] CreatePluginConstructorTypes();

        protected abstract object[] CreatePluginConstructorArguments();

        private IEnumerable<string> GetPluginCandidateFiles(string directory)
        {
            return Directory.GetFiles(directory, "*plugin*.dll");
        }

        private Assembly CreateAssembly(string pathToAssembly)
        {
            var assembly = Assembly.LoadFile(pathToAssembly);
            return assembly;
        }

        private TPlugin CreatePlugin(Assembly assembly, Type type)
        {
            var constructor = type.GetConstructor(CreatePluginConstructorTypes());
            if (constructor == null)
            {
                throw new InvalidOperationException(string.Format("The plugin '{0}' does not have a constructor with the correct paramaters.", type.FullName));
            }

            var instance = (TPlugin)constructor.Invoke(CreatePluginConstructorArguments());

            return instance;
        }
        #endregion
    }
}
