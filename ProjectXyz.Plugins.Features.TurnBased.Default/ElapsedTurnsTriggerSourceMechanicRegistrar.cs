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
            ArgumentContract.RequiresNotNull(
                triggerMechanic,
                nameof(triggerMechanic));
            return triggerMechanic is IElapsedTurnsTriggerMechanic;
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            ArgumentContract.RequiresNotNull(
                triggerMechanic,
                nameof(triggerMechanic));
            _elapsedTimeTriggerMechanics.Add((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            ArgumentContract.RequiresNotNull(
                triggerMechanic,
                nameof(triggerMechanic));
            _elapsedTimeTriggerMechanics.Remove((IElapsedTurnsTriggerMechanic)triggerMechanic);
        }

        public async Task UpdateAsync(ITurnInfo turnInfo)
        {
            ArgumentContract.RequiresNotNull(
                turnInfo,
                nameof(turnInfo));
            
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