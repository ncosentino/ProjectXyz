using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Systems
{
    public interface ISystem
    {
        void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects);
    }
}