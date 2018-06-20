using System;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration
{
    public sealed class ExpiryTriggerMechanicFactory : IExpiryTriggerMechanicFactory
    {
        private readonly IDurationTriggerMechanicFactory _durationTriggerMechanicFactory;

        public ExpiryTriggerMechanicFactory(IDurationTriggerMechanicFactory durationTriggerMechanicFactory)
        {
            _durationTriggerMechanicFactory = durationTriggerMechanicFactory;
        }

        public ITriggerMechanic Create(
            IExpiryTriggerBehavior expiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            var triggerComponent = expiryTriggerBehavior.TriggerBehavior;
            if (triggerComponent is IDurationTriggerBehavior)
            {
                return _durationTriggerMechanicFactory.Create(
                    (IDurationTriggerBehavior)triggerComponent,
                    triggeredCallback);
            }

            throw new NotSupportedException($"No factory is implemented for '{triggerComponent}'.");
        }
    }
}