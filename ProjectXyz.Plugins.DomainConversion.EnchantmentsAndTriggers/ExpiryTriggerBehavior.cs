using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.TurnBased.Duration;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public sealed class ExpiryTriggerBehavior :
        BaseBehavior,
        IExpiryTriggerBehavior
    {
        public ExpiryTriggerBehavior(IDurationInTurnsTriggerBehavior triggerBehavior)
        {
            Contract.Requires(
                triggerBehavior.DurationInTurns > 0,
                $"{nameof(triggerBehavior)} must have a duration greater than " +
                $"zero. If your intention is to have an instantaneous trigger " +
                $"(i.e. an enchantment to be used for a single action), you " +
                $"may want to consider using an action trigger behavior instead.");
            TriggerBehavior = triggerBehavior;
        }

        public ExpiryTriggerBehavior(IDurationInActionsTriggerBehavior triggerBehavior)
        {
            Contract.Requires(
                triggerBehavior.DurationInActions > 0,
                $"{nameof(triggerBehavior)} must have a duration greater than " +
                $"zero.");
            TriggerBehavior = triggerBehavior;
        }

        public IBehavior TriggerBehavior { get; }
    }
}