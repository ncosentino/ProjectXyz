using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Game.Interface.Mapping;

namespace ProjectXyz.Game.Core.Mapping
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