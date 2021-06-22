using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapRepositoryFacade : IMapRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableMapRepository> _repositories;

        public MapRepositoryFacade(IEnumerable<IDiscoverableMapRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public async Task<IReadOnlyCollection<IGameObject>> LoadMapsAsync(IFilterContext filterContext)
        {
            var maps = (await SelectManyAsync(_repositories, x => x.LoadMapsAsync(filterContext))).ToArray();
            return maps;
        }

        private static async Task<IEnumerable<T1>> SelectManyAsync<T, T1>(IEnumerable<T> enumeration, Func<T, Task<IReadOnlyCollection<T1>>> func)
        {
            return (await Task.WhenAll(enumeration.Select(func)).ConfigureAwait(false)).SelectMany(s => s);
        }
    }
}