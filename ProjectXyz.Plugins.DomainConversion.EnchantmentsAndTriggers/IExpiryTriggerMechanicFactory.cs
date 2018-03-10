using System;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerComponent expiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback);
    }
}