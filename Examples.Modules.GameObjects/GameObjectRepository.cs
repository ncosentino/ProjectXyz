using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.GameObjects;

namespace Examples.Modules.GameObjects
{
    public sealed class GameObjectRepository : IGameObjectRepository
    {
        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            yield break;
        }
    }
}
