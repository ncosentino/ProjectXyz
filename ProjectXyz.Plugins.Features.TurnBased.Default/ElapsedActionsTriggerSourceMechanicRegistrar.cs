using System.Collections.Generic;
using System.Threading.Tasks;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ElapsedActionsTriggerSourceMechanicRegistrar : IElapsedActionsTriggerSourceMechanicRegistrar
    {
        private readonly List<IElapsedActionsTriggerMechanic> _elapsedTimeTriggerMechanics;

        public ElapsedActionsTriggerSourceMechanicRegistrar()
        {
            _elapsedTimeTriggerMechanics = new List<IElapsedActionsTriggerMechanic>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic)
        {
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            return triggerMechanic is IElapsedActionsTriggerMechanic;
        }

        public void RegisterTrigger(ITriggerMechanic triggerMechanic)
        {
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            _elapsedTimeTriggerMechanics.Add((IElapsedActionsTriggerMechanic)triggerMechanic);
        }

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic)
        {
            Contract.RequiresNotNull(
                triggerMechanic,
                $"{nameof(triggerMechanic)} cannot be null.");
            _elapsedTimeTriggerMechanics.Remove((IElapsedActionsTriggerMechanic)triggerMechanic);
        }

        public async Task UpdateAsync(IActionInfo actionInfo)
        {
            Contract.RequiresNotNull(
                actionInfo,
                $"{nameof(actionInfo)} cannot be null.");

            // FIXME: can this be done in parallel? does that have weird side effects?
            var snapshot = _elapsedTimeTriggerMechanics.ToArray();
            foreach (var trigger in snapshot)
            {
                if (!await trigger
                    .UpdateAsync(actionInfo)
                    .ConfigureAwait(false))
                {
                    _elapsedTimeTriggerMechanics.Remove(trigger);
                }
            }
        }
    }
}