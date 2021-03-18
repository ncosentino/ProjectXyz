
using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
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

        public IMap LoadMap(IIdentifier mapId)
        {
            var map = _repositories
                .Select(x => x.LoadMap(mapId))
                .FirstOrDefault(x => x != null);
            if (map == null)
            {
                throw new InvalidOperationException(
                    $"Could not get an instance of '{typeof(IMap)}' for ID '{mapId}'.");
            }

            return map;
        }
    }
}