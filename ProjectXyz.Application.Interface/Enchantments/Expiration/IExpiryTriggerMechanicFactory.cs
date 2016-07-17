using System;
using ProjectXyz.Application.Interface.Triggering;

namespace ProjectXyz.Application.Interface.Enchantments.Expiration
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerComponent expiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}