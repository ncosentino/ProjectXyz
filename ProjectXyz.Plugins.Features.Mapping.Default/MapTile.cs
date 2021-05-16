using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapTile : IMapTile
    {
        public MapTile(
            int x,
            int y,
            IEnumerable<IBehavior> behaviors)
        {
            X = x;
            Y = y;
            Behaviors = behaviors.ToArray();
        }

        public int X { get; }

        public int Y { get; }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}