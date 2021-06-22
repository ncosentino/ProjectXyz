using System.Collections.Generic;

using NexusLabs.Contracts;

using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
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
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            return triggerMechanic is IElapsedTurnsTriggerMechanic;
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            _elapsedTimeTriggerMechanics.Add((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            _elapsedTimeTriggerMechanics.Remove((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public void Update(ITurnInfo turnInfo)
        {
            Contract.RequiresNotNull(
                turnInfo,
                $"{nameof(turnInfo)} cannot be null.");
            _elapsedTimeTriggerMechanics.RemoveAll(x => !x.Update(turnInfo));
        }
    }
}