using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginProvider : IPluginProvider
    {
        #region Fields
        private readonly Dictionary<Type, IPluginRepository> _pluginRepositories;
        #endregion

        #region Constructors
        public PluginProvider(IReadOnlyCollection<IPluginRepository> pluginRepositories)
        {
            _pluginRepositories = new Dictionary<Type, IPluginRepository>();

            foreach (var pluginRepository in pluginRepositories)
            {
                var pluginType = pluginRepository
                    .GetType()
                    .GetInterfaces()
                    .Where(x => x.IsGenericType && x.GenericTypeArguments.Length == 1)
                    .Select(x => x.GenericTypeArguments.Single(type => typeof(IPlugin).IsAssignableFrom(type)))
                    .Single();
                _pluginRepositories.Add(pluginType, pluginRepository);
            }
        }
        #endregion

        #region Methods
        public IEnumerable<TPlugin> GetPlugins<TPlugin>() 
            where TPlugin : IPlugin
        {
            return ((IPluginRepository<TPlugin>)_pluginRepositories[typeof(TPlugin)]).Plugins;
        }
        #endregion
    }
}
