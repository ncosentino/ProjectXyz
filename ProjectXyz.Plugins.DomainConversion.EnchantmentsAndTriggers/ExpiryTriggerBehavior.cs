using ProjectXyz.Api.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration
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