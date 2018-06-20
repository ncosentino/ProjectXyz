using System;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationTriggerBehavior elapsedTimeExpiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}