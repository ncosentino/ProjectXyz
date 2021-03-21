using System.Collections.Generic;

using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class ElapsedTurnsTriggerSourceMechanicRegistrar : IElapsedTurnsTriggerSourceMechanicRegistrar
    {
        private readonly List<IElapsedTurnsTriggerMechanic> _elapsedTimeTriggerMechanics;

        public ElapsedTurnsTriggerSourceMechanicRegistrar()
        {
            _elapsedTimeTriggerMechanics = new List<IElapsedTurnsTriggerMechanic>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic)
        {
            return triggerMechanic is IElapsedTurnsTriggerMechanic;
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            _elapsedTimeTriggerMechanics.Add((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            _elapsedTimeTriggerMechanics.Remove((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public void Update(ITurnInfo turnInfo)
        {
            _elapsedTimeTriggerMechanics.RemoveAll(x => !x.Update(turnInfo));
        }
    }
}