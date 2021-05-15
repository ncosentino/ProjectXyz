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

            foreach (var behavior in behaviors)
            {
                _behaviors.Add(behavior);

                if (behavior is IRegisterableBehavior registerableBehavior)
                {
                    registerableBehavior.RegisteringToOwner(this);
                }
            }

            // RegisteredToOwner executes after all the RegisteringToOwner calls
            foreach (var behavior in _behaviors.TakeTypes<IRegisterableBehavior>())
            {
                behavior.RegisteredToOwner(this);
            }
        }

        public IReadOnlyCollection<IBehavior> Behaviors => _behaviors;
    }
}