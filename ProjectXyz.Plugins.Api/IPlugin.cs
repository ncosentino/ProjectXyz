using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Plugins.Api
{
    public interface IPlugin
    {
        IComponentCollection SharedComponents { get; }
    }
}
