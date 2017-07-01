using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginLoaderResult
    {
        IReadOnlyCollection<IPlugin> Plugins { get; }

        IComponentCollection Components { get; }
    }
}
