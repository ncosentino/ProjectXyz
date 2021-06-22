using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapLayersBehavior : 
        BaseBehavior,
        IMapLayersBehavior
    {
        public MapLayersBehavior(IEnumerable<IMapLayer> layers)
        {
            Layers = layers.ToArray();
        }

        public IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}