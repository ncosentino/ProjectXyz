using System.Collections.Generic;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMap
    {
        IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}