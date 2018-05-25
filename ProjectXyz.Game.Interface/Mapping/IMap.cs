using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMap
    {
        IIdentifier Id { get; }

        IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}