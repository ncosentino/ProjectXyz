using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapLayer
    {
        string Name { get; }

        IReadOnlyCollection<IGameObject> Tiles { get; }
    }
}