using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapGameObjectRepository
    {
        IEnumerable<IGameObject> LoadForMap(IIdentifier mapId);

        void SaveState(
            IGameObject map,
            IEnumerable<IGameObject> gameObjects);
    }
}
