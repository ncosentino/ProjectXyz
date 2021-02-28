using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapLayer
    {
        string Name { get; }

        IReadOnlyCollection<IMapTile> Tiles { get; }
    }
}