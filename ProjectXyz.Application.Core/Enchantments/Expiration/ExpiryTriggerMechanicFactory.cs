using System;
using ProjectXyz.Application.Interface.Enchantments.Expiration;
using ProjectXyz.Application.Interface.Triggering;
using ProjectXyz.Application.Interface.Triggering.Triggers.Duration;

namespace ProjectXyz.Application.Core.Enchantments.Expiration
{
    public sealed class ExpiryTriggerMechanicFactory : IExpiryTriggerMechanicFactory
    {
        private readonly IDurationTriggerMechanicFactory _durationTriggerMechanicFactory;

        public ExpiryTriggerMechanicFactory(IDurationTriggerMechanicFactory durationTriggerMechanicFactory)
        {
            _durationTriggerMechanicFactory = durationTriggerMechanicFactory;
        }

        public ITriggerMechanic Create(
            IExpiryTriggerComponent expiryTriggerComponent,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var triggerComponent = expiryTriggerComponent.TriggerComponent;
            if (triggerComponent is IDurationTriggerComponent)
            {
                return _durationTriggerMechanicFactory.Create(
                    (IDurationTriggerComponent)triggerComponent,
                    triggeredCallback);
            }

            throw new NotSupportedException($"No factory is implemented for '{triggerComponent}'.");
        }
    }
}