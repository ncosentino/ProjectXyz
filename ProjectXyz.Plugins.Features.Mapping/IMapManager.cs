using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapManager : IMapProvider
    {
        Task UnloadMapAsync();

        Task SwitchMapAsync(IIdentifier mapId);

        Task SwitchMapAsync(IFilterContext filterContext);

        Task SaveActiveMapStateAsync();
    }
}