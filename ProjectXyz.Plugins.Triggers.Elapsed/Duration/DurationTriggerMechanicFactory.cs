using System;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Duration
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