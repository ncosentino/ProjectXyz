using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Game.Interface.Mapping;

namespace ProjectXyz.Game.Core.Mapping
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