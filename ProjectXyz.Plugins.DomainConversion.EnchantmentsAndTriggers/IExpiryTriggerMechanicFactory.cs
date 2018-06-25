using System;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public interface IExpiryTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IExpiryTriggerBehavior expiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}