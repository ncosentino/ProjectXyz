using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> LoadForMap(IIdentifier mapId);
    }
}
