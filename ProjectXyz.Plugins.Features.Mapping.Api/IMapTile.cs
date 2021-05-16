using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapTile
    {
        int X { get; }

        int Y { get; }

        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}