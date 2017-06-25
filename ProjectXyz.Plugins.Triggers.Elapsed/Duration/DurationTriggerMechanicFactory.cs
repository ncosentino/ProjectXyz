using System;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Duration
{
    public sealed class DurationTriggerMechanicFactory : IDurationTriggerMechanicFactory
    {
        public ITriggerMechanic Create(
            IDurationTriggerComponent elapsedTimeExpiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var trigger = new DurationTriggerMechanic(
                elapsedTimeExpiryTriggerComponent,
                (t, _) => triggeredCallback(t));
            return trigger;
        }
    }
}