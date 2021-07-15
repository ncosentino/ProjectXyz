using System;

using ProjectXyz.Plugins.Features.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationInActionsTriggerMechanicFactory : IDurationInActionsTriggerMechanicFactory
    {
        public ITriggerMechanic Create(
            IDurationInActionsTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var trigger = new ElapsedActionsTriggerMechanic(
                durationTriggerBehavior,
                (t, _) => triggeredCallback(t));
            return trigger;
        }
    }
}