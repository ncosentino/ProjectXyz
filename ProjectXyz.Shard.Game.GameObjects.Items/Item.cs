using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Behaviors;

namespace ProjectXyz.Shared.Game.GameObjects.Items
{
    public sealed class Item : IGameObject
    {
        public Item(
            IBehaviorManager behaviorManager,
            IHasMutableStatsBehavior hasStatsBehavior, // FIXME: this seems bad because it depends on a feature...
            IEnumerable<IBehavior> behaviors)
        {
            Behaviors = new BehaviorCollection(behaviors.Append(hasStatsBehavior));
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}