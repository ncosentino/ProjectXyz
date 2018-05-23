using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorCollectionFactory
    {
        IBehaviorCollection Create(params IBehavior[] behaviors);

        IBehaviorCollection Create(IEnumerable<IBehavior> behaviors);
    }
}