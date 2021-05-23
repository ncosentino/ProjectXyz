using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapLayer
    {
        string Name { get; }

        IReadOnlyCollection<IGameObject> Tiles { get; }
    }
}