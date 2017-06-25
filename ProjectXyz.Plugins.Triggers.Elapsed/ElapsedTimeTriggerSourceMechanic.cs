using System.Collections.Generic;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public sealed class ElapsedTimeTriggerSourceMechanic :
        IElapsedTimeTriggerSourceMechanic,
        ITriggerMechanicRegistrar
    {
        private readonly List<IElapsedTimeTriggerMechanic> _elapsedTimeTriggerMechanics;

        public ElapsedTimeTriggerSourceMechanic()
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