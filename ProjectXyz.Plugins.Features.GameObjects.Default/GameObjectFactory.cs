using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Behaviors.Default
{
    public sealed class GameObjectFactory : IGameObjectFactory
    {
        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var gameObject = new GameObject(behaviors);
            return gameObject;
        }
    }
}