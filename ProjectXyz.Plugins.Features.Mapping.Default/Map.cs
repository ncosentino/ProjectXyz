using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{

    public sealed class Map : IMap
    {
        public Map(
            IIdentifier id,
            IEnumerable<IMapLayer> layers,
            IEnumerable<IBehavior> behaviors)
        {
            Id = id;
            Layers = layers.ToArray();
            Behaviors = behaviors.ToArray();
        }

        public IIdentifier Id { get; }

        public IReadOnlyCollection<IMapLayer> Layers { get; }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}