using ProjectXyz.Api.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class BehaviorManager : IBehaviorManager
    {
        public void Register(
            IHasBehaviors owner,
            IBehaviorCollection behaviors)
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