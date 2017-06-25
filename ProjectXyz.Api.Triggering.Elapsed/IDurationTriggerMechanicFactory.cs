using System;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationTriggerComponent elapsedTimeExpiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}