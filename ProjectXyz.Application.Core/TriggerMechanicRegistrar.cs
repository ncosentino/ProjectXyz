using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Shared.Triggering
{
    public sealed class TriggerMechanicRegistrar : ITriggerMechanicRegistrar
    {
        private readonly IReadOnlyCollection<ITriggerMechanicRegistrar> _triggerMechanicRegistrars;

        public TriggerMechanicRegistrar(IEnumerable<ITriggerMechanicRegistrar> triggerMechanicRegistrars)
        {
            _triggerMechanicRegistrars = triggerMechanicRegistrars.ToArray();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic)
        {
            return _triggerMechanicRegistrars.Any(x => x.CanRegister(triggerMechanic));
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            var registrars = _triggerMechanicRegistrars
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
            foreach (var registrar in _triggerMechanicRegistrars)
            {
                registrar.UnregisterTrigger(triggerMechanic);
            }
        }
    }
}