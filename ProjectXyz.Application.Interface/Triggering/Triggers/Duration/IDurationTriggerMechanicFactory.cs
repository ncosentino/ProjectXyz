using System;

namespace ProjectXyz.Application.Interface.Triggering.Triggers.Duration
{
    public interface IDurationTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationTriggerComponent elapsedTimeExpiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}