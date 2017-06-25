using System;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerComponent expiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}