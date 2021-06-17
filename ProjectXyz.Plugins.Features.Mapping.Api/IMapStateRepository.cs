using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapStateRepository
    {
        IEnumerable<KeyValuePair<IIdentifier, IReadOnlyCollection<IIdentifier>>> GetAllState();

        IEnumerable<IIdentifier> GetState(IIdentifier mapId);

        bool HasState(IIdentifier mapId);

        void SaveState(
            IGameObject map,
            IEnumerable<IGameObject> gameObjects);

        void ClearState();
    }
}
