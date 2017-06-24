using System;
using ProjectXyz.Application.Interface.Triggering;

namespace ProjectXyz.Application.Enchantments.Interface.Expiration
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerComponent expiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}