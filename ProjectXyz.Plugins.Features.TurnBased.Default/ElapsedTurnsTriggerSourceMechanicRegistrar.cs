using System.Collections.Generic;
using System.Threading.Tasks;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Triggering;

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

        public async Task UpdateAsync(ITurnInfo turnInfo)
        {
            Contract.RequiresNotNull(
                turnInfo,
                $"{nameof(turnInfo)} cannot be null.");
            
            // FIXME: can this be done in parallel? does that have weird side effects?
            var snapshot = _elapsedTimeTriggerMechanics.ToArray();
            foreach (var trigger in snapshot)
            {
                if (!await trigger
                    .UpdateAsync(turnInfo)
                    .ConfigureAwait(false))
                {
                    _elapsedTimeTriggerMechanics.Remove(trigger);
                }
            }
        }
    }
}