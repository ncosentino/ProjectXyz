using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Game.Core.GameObjects.Items
{
    public sealed class Item : IGameObject
    {
        public Item(
            IBehaviorManager behaviorManager,
            IHasMutableStatsBehavior hasStatsBehavior,
            IEnumerable<IBehavior> behaviors)
        {
            Behaviors = new BehaviorCollection(behaviors.Append(hasStatsBehavior));
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}