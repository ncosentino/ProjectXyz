using System.Collections;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class OwnedBehaviorCollection : IBehaviorCollection
    {
        private readonly IBehaviorCollection _behaviorCollection;

        public OwnedBehaviorCollection(
            IHasBehaviors owner,
            IBehaviorCollection behaviorCollection)
        {
            _behaviorCollection = behaviorCollection;

            foreach (var behavior in behaviorCollection)
            {
                behavior.RegisterTo(owner);
            }
        }

        public IEnumerator<IBehavior> GetEnumerator() => _behaviorCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _behaviorCollection.GetEnumerator();

        public int Count => _behaviorCollection.Count;

        public IEnumerable<TBehavior> Get<TBehavior>()
            where TBehavior : IBehavior =>
            _behaviorCollection.Get<TBehavior>();
    }
}