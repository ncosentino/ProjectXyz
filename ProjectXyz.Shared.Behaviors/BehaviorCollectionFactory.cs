using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Shared.Behaviors
{
    public sealed class BehaviorCollectionFactory : IBehaviorCollectionFactory
    {
        public IBehaviorCollection Create(params IBehavior[] behaviors) => Create((IEnumerable<IBehavior>)behaviors);

        public IBehaviorCollection Create(IEnumerable<IBehavior> behaviors) => new BehaviorCollection(behaviors);
    }
}