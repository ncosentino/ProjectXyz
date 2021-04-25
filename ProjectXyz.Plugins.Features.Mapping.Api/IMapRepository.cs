using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapRepository
    {
        IEnumerable<IMap> LoadMaps(IFilterContext filterContext);
    }
}