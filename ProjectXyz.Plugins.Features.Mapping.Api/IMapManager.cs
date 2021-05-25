using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapManager : IMapProvider
    {
        void UnloadMap();

        void SwitchMap(IIdentifier mapId);

        void SwitchMap(IFilterContext filterContext);
    }
}