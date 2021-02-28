using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMap : IGameObject
    {
        IIdentifier Id { get; }

        IReadOnlyCollection<IMapLayer> Layers { get; }
    }
}