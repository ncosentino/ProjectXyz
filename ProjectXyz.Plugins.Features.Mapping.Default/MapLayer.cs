using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapLayer : IMapLayer
    {
        public MapLayer(
            string name,
            IEnumerable<IGameObject> tiles)
        {
            Name = name;
            Tiles = tiles.ToArray();
        }

        public string Name { get; }

        public IReadOnlyCollection<IGameObject> Tiles { get; }
    }
}