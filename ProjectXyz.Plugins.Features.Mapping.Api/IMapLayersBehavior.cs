using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapLayersBehavior : IBehavior
    {
        IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}