using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapTile
    {
        int X { get; }

        int Y { get; }

        IReadOnlyCollection<ITileComponent> Components { get; }
    }
}