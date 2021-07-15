using System;

using ProjectXyz.Plugins.Features.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationInTurnsTriggerMechanicFactory : IDurationInTurnsTriggerMechanicFactory
    {
        public ITriggerMechanic Create(
            IDurationInTurnsTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var trigger = new ElapsedTurnsTriggerMechanic(
                durationTriggerBehavior,
                (t, _) => triggeredCallback(t));
            return trigger;
        }
    }
}