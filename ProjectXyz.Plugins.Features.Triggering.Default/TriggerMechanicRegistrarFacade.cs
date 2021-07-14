using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.Triggering.Default
{
    public sealed class TriggerMechanicRegistrarFacade : ITriggerMechanicRegistrarFacade
    {
        private readonly Lazy<IReadOnlyCollection<ITriggerMechanicRegistrar>> _triggerMechanicRegistrars;

        public TriggerMechanicRegistrarFacade(
            Lazy<IEnumerable<IDiscoverableTriggerMechanicRegistrar>> triggerMechanicRegistrars,
            IEnumerable<IDiscoverableTriggerMechanic> triggerMechanics)
        {
            _triggerMechanicRegistrars = new Lazy<IReadOnlyCollection<ITriggerMechanicRegistrar>>(() =>
                triggerMechanicRegistrars.Value.ToArray());
            foreach (var triggerMechanic in triggerMechanics)
            {
                RegisterTrigger(triggerMechanic);
            }
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic) =>
            _triggerMechanicRegistrars
                .Value
                .Any(x => x.CanRegister(triggerMechanic));

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            var registrars = _triggerMechanicRegistrars
                .Value
                .Where(x => x.CanRegister(triggerMechanic));
            foreach (var registrar in registrars)
            {
                registrar.RegisterTrigger(triggerMechanic);
            }
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            var registrars = _triggerMechanicRegistrars
                .Value
                .Where(x => x.CanRegister(triggerMechanic));
            foreach (var registrar in registrars)
            {
                registrar.UnregisterTrigger(triggerMechanic);
            }
        }
    }
}