using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapManager : IMapProvider
    {
        void UnloadMap();

        void SwitchMap(IIdentifier mapId);

        void SwitchMap(IFilterContext filterContext);
    }
}