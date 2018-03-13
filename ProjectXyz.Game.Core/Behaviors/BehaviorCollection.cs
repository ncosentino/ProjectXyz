using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class BehaviorCollection : IBehaviorCollection
    {
        #region Fields
        private readonly List<IBehavior> _behaviors;
        #endregion

        #region Constructors
        public BehaviorCollection(
            IBehavior behavior, 
            params IBehavior[] behaviors)
            : this(behavior.Yield().Concat(behaviors))
        {
        }

        public BehaviorCollection(IEnumerable<IBehavior> behaviors)
        {
            _behaviors = new List<IBehavior>(behaviors);
        }
        #endregion

        #region Properties
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