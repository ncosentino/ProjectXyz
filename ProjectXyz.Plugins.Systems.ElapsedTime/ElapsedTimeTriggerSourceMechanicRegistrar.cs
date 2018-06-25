using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.ElapsedTime.Api;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public sealed class ElapsedTimeTriggerSourceMechanicRegistrar : IElapsedTimeTriggerSourceMechanicRegistrar
    {
        private readonly List<IElapsedTimeTriggerMechanic> _elapsedTimeTriggerMechanics;

        public ElapsedTimeTriggerSourceMechanicRegistrar()
        {
            _elapsedTimeTriggerMechanics = new List<IElapsedTimeTriggerMechanic>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic)
        {
            return triggerMechanic is IElapsedTimeTriggerMechanic;
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            _elapsedTimeTriggerMechanics.Add((IElapsedTimeTriggerMechanic)triggerMechanic);
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            _elapsedTimeTriggerMechanics.Remove((IElapsedTimeTriggerMechanic)triggerMechanic);
        }

        public void Update(IInterval elapsed)
        {
            _elapsedTimeTriggerMechanics.RemoveAll(x => !x.Update(elapsed));
        }
    }
}