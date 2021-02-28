using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapManager : IMapProvider
    {
        void SwitchMap(IIdentifier mapId);
    }
}