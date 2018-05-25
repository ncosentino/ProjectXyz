using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;

namespace ProjectXyz.Game.Core.Mapping
{
    public sealed class Map : IMap
    {
        public Map(
            IIdentifier id,
            IEnumerable<IMapLayer> layers)
        {
            Id = id;
            Layers = layers.ToArray();
        }

        public IIdentifier Id { get; }

        public IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}