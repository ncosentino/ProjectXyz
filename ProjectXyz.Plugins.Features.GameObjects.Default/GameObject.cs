using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Behaviors.Default
{
    public sealed class GameObject : IGameObject
    {
        private readonly List<IBehavior> _behaviors;

        public GameObject(IEnumerable<IBehavior> behaviors)
        {
            _behaviors = new List<IBehavior>();

            foreach (var behavior in behaviors.TakeTypes<IRegisterableBehavior>())
            {
                _behaviors.Add(behavior);
                behavior.RegisteringToOwner(this);
            }

            foreach (var behavior in behaviors.TakeTypes<IRegisterableBehavior>())
            {
                behavior.RegisteredToOwner(this);
            }
        }

        public IReadOnlyCollection<IBehavior> Behaviors => _behaviors;
    }
}