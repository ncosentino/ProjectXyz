using System.Collections.Generic;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapTile
    {
        int X { get; }

        int Y { get; }

        IReadOnlyCollection<ITileComponent> Components { get; }
    }
}