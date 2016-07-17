using System;
using ProjectXyz.Application.Interface.Triggering;
using ProjectXyz.Application.Interface.Triggering.Triggers.Duration;

namespace ProjectXyz.Application.Core.Triggering.Triggers.Duration
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