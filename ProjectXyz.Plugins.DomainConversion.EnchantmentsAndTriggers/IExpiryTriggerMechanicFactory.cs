using System;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerBehavior expiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}