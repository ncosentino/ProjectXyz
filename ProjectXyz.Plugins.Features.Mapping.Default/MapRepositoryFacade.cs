using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapRepositoryFacade : IMapRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableMapRepository> _repositories;

        public MapRepositoryFacade(IEnumerable<IDiscoverableMapRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IGameObject> LoadMaps(IFilterContext filterContext)
        {
            var maps = _repositories
                .SelectMany(x => x.LoadMaps(filterContext));
            return maps;
        }
    }
}