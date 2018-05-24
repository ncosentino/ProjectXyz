using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class Actor : IGameObject
    {
        public Actor(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IBehaviorManager behaviorManager,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            IBuffableBehavior buffableBehavior,
            IHasMutableStatsBehavior hasStatsBehavior,
            ICanEquipBehavior canEquipBehavior,
            IApplyEquipmentEnchantmentsBehavior applyEquipmentEnchantmentsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            Behaviors = behaviorCollectionFactory.Create(new IBehavior[]
                {
                    hasEnchantmentsBehavior,
                    buffableBehavior,
                    hasStatsBehavior,
                    canEquipBehavior,
                    applyEquipmentEnchantmentsBehavior
                }
                .Concat(additionalBehaviors));
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}