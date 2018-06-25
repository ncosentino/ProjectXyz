using System;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Api.Duration
{
    public interface IDurationTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationTriggerBehavior elapsedTimeExpiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}