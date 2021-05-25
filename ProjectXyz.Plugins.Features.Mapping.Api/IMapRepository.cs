using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapRepository
    {
        IEnumerable<IGameObject> LoadMaps(IFilterContext filterContext);
    }
}