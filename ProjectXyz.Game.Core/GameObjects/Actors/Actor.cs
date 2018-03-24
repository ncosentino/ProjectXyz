using ProjectXyz.Api.Behaviors;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.GameObjects;

namespace ProjectXyz.Game.Core.GameObjects.Actors
{
    public sealed class Actor : IGameObject
    {
        public Actor(
            IBehaviorManager behaviorManager,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            IBuffableBehavior buffableBehavior,
            IHasMutableStatsBehavior hasStatsBehavior,
            ICanEquipBehavior canEquipBehavior,
            IApplyEquipmentEnchantmentsBehavior applyEquipmentEnchantmentsBehavior)
        {
            Behaviors = new BehaviorCollection(
                hasEnchantmentsBehavior,
                buffableBehavior,
                hasStatsBehavior,
                canEquipBehavior,
                applyEquipmentEnchantmentsBehavior);
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}