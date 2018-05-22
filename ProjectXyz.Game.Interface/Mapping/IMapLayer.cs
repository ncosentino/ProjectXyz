using System.Collections.Generic;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapLayer
    {
        string Name { get; }

        IReadOnlyCollection<IMapTile> Tiles { get; }
    }
}