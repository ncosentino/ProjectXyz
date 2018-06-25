using System;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.ElapsedTime.Api.Duration;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Duration
{
    public sealed class DurationTriggerMechanicFactory : IDurationTriggerMechanicFactory
    {
        public ITriggerMechanic Create(
            IDurationTriggerBehavior elapsedTimeExpiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var trigger = new DurationTriggerMechanic(
                elapsedTimeExpiryTriggerBehavior,
                (t, _) => triggeredCallback(t));
            return trigger;
        }
    }
}