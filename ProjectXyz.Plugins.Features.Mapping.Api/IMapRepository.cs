using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapRepository
    {
        IEnumerable<IGameObject> LoadMaps(IFilterContext filterContext);
    }
}