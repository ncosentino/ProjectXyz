using System;

using ProjectXyz.Plugins.Features.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public sealed class ExpiryTriggerMechanicFactory : IExpiryTriggerMechanicFactory
    {
        private readonly IDurationInTurnsTriggerMechanicFactory _durationInTurnsTriggerMechanicFactory;
        private readonly IDurationInActionsTriggerMechanicFactory _durationInActionsTriggerMechanicFactory;

        public ExpiryTriggerMechanicFactory(
            IDurationInTurnsTriggerMechanicFactory durationInTurnsTriggerMechanicFactory,
            IDurationInActionsTriggerMechanicFactory durationInActionsTriggerMechanicFactory)
        {
            _durationInTurnsTriggerMechanicFactory = durationInTurnsTriggerMechanicFactory;
            _durationInActionsTriggerMechanicFactory = durationInActionsTriggerMechanicFactory;
        }

        public ITriggerMechanic Create(
            IExpiryTriggerBehavior expiryTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback)
        {
            // FIXME: this is typical facade setup...
            var triggerBehavior = expiryTriggerBehavior.TriggerBehavior;
            if (triggerBehavior is IDurationInTurnsTriggerBehavior durationInTurnsTriggerBehavior)
            {
                return _durationInTurnsTriggerMechanicFactory.Create(
                    durationInTurnsTriggerBehavior,
                    triggeredCallback);
            }

            if (triggerBehavior is IDurationInActionsTriggerBehavior durationInActionsTriggerBehavior)
            {
                return _durationInActionsTriggerMechanicFactory.Create(
                    durationInActionsTriggerBehavior,
                    triggeredCallback);
            }

            throw new NotSupportedException($"No factory is implemented for '{triggerBehavior}'.");
        }
    }
}