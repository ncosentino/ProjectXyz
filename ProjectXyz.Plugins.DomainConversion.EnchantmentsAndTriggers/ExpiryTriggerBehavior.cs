using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public sealed class ExpiryTriggerBehavior :
        BaseBehavior,
        IExpiryTriggerBehavior
    {
        public ExpiryTriggerBehavior(IBehavior triggerBehavior)
        {
            TriggerBehavior = triggerBehavior;
        }

        public IBehavior TriggerBehavior { get; }
    }
}