using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapLayer : IMapLayer
    {
        public MapLayer(
            string name,
            IEnumerable<IMapTile> tiles)
        {
            Name = name;
            Tiles = tiles.ToArray();
        }

        public string Name { get; }

        public IReadOnlyCollection<IMapTile> Tiles { get; }
    }
}