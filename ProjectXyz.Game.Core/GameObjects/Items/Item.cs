using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.GameObjects.Items
{
    public sealed class Item : IGameObject
    {
        public Item(
            IBehaviorManager behaviorManager,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            IBuffableBehavior buffableBehavior,
            IHasMutableStatsBehavior hasStatsBehavior,
            ICanBeEquippedBehavior canBeEquippedBehavior)
        {
            Behaviors = new BehaviorCollection(
                hasEnchantmentsBehavior,
                buffableBehavior,
                hasStatsBehavior,
                canBeEquippedBehavior);
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}