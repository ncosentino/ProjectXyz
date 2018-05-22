using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Game.Interface.Mapping;

namespace ProjectXyz.Game.Core.Mapping
{
    public sealed class Map : IMap
    {
        public Map(IEnumerable<IMapLayer> layers)
        {
            Layers = layers.ToArray();
        }

        public IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}