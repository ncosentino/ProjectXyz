using System;

using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationTriggerMechanicFactory : IDurationTriggerMechanicFactory
    {
        public ITriggerMechanic Create(
            IDurationTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var trigger = new DurationTriggerMechanic(
                durationTriggerBehavior,
                (t, _) => triggeredCallback(t));
            return trigger;
        }
    }
}