using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class BehaviorCollection : IBehaviorCollection
    {
        #region Fields
        private readonly List<IBehavior> _behaviors;
        #endregion

        #region Constructors
        public BehaviorCollection(IBehavior behavior, params IBehavior[] behaviors)
            : this(behavior.Yield().Concat(behaviors))
        {
        }

        public BehaviorCollection(IEnumerable<IBehavior> behaviors)
        {
            _behaviors = new List<IBehavior>(behaviors);
        }

        private BehaviorCollection()
            : this(Enumerable.Empty<IBehavior>())
        {
        }
        #endregion

        #region Properties
        public static IBehaviorCollection Empty { get; } = new BehaviorCollection();

        public int Count => _behaviors.Count;
        #endregion

        #region Methods
        public IEnumerable<TBehavior> Get<TBehavior>() 
            where TBehavior : IBehavior
        {
            return _behaviors.TakeTypes<TBehavior>();
        }

        public IEnumerator<IBehavior> GetEnumerator()
        {
            return _behaviors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}