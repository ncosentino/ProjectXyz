using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapTile : IMapTile
    {
        public MapTile(
            int x,
            int y,
            IEnumerable<ITileComponent> components)
        {
            X = x;
            Y = y;
            Components = components.ToArray();
        }

        public int X { get; }

        public int Y { get; }


        public IReadOnlyCollection<ITileComponent> Components { get; }
    }
}