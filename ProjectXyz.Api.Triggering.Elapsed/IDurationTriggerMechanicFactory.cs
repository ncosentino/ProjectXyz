using System;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerMechanicFactory : IComponent
    {
        ITriggerMechanic Create(
            IDurationTriggerComponent elapsedTimeExpiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}