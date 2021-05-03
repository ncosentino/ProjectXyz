using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Behaviors.Default
{
    public sealed class GameObjectFactory : IGameObjectFactory
    {
        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var gameObject = new GameObject(behaviors);

            foreach (var behavior in behaviors.TakeTypes<IRegisterableBehavior>())
            {
                behavior.RegisteringToOwner(gameObject);
            }

            foreach (var behavior in behaviors.TakeTypes<IRegisterableBehavior>())
            {
                behavior.RegisteredToOwner(gameObject);
            }

            return gameObject;
        }
    }
}