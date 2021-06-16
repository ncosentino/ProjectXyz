using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapManager : IMapProvider
    {
        void UnloadMap();

        Task SwitchMapAsync(IIdentifier mapId);

        Task SwitchMapAsync(IFilterContext filterContext);
    }
}