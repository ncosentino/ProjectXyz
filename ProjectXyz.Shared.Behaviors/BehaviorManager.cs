using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.Behaviors.Default
{
    public sealed class BehaviorManager : IBehaviorManager
    {
        public void Register(
            IHasBehaviors owner,
            IReadOnlyCollection<IBehavior> behaviors)
        {
            foreach (var behavior in behaviors)
            {
                behavior.RegisteringToOwner(owner);
            }

            foreach (var behavior in behaviors)
            {
                behavior.RegisteredToOwner(owner);
            }
        }
    }
}