using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorCollection : IReadOnlyCollection<IBehavior>
    {
        IEnumerable<TBehavior> Get<TBehavior>()
            where TBehavior : IBehavior;
    }
}