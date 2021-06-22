using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.Triggering.Default
{
    public sealed class TriggerMechanicRegistrarFacade : ITriggerMechanicRegistrarFacade
    {
        private readonly Lazy<IReadOnlyCollection<ITriggerMechanicRegistrar>> _triggerMechanicRegistrars;

        public TriggerMechanicRegistrarFacade(Lazy<IEnumerable<IDiscoverableTriggerMechanicRegistrar>> triggerMechanicRegistrars)
        {
            _triggerMechanicRegistrars = new Lazy<IReadOnlyCollection<ITriggerMechanicRegistrar>>(() =>
                triggerMechanicRegistrars.Value.ToArray());
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic) =>
            _triggerMechanicRegistrars
                .Value
                .Any(x => x.CanRegister(triggerMechanic));

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            var registrars = _triggerMechanicRegistrars
                .Value
                .Where(x =>
                    triggerMechanic.CanBeRegisteredTo(x) &&
                    x.CanRegister(triggerMechanic));
            foreach (var registrar in registrars)
            {
                registrar.RegisterTrigger(triggerMechanic);
            }
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            foreach (var registrar in _triggerMechanicRegistrars.Value)
            {
                registrar.UnregisterTrigger(triggerMechanic);
            }
        }
    }
}