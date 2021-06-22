using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using System.Threading.Tasks;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapRepository
    {
        Task<IReadOnlyCollection<IGameObject>> LoadMapsAsync(IFilterContext filterContext);
    }
}