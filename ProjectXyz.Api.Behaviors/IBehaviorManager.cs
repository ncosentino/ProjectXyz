using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorManager
    {
        void Register(
            IHasBehaviors owner,
            IReadOnlyCollection<IBehavior> behaviors);
    }
}