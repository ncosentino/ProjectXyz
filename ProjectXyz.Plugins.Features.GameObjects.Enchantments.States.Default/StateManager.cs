using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class StateManager : IStateManager
    {
        private readonly ConcurrentDictionary<IIdentifier, Dictionary<IIdentifier, object>> _state;

        public StateManager()
        {
            _state = new ConcurrentDictionary<IIdentifier, Dictionary<IIdentifier, object>>();
        }

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public IReadOnlyDictionary<IIdentifier, object> GetStates(IIdentifier stateTypeId) =>
            _state.TryGetValue(stateTypeId, out var states)
                ? states
                : new Dictionary<IIdentifier, object>();

        public void SetState(
            IIdentifier stateTypeId,
            IIdentifier stateId,
            double value) => SetStates(
                stateTypeId,
                new[] { Tuple.Create(stateId, (object)value) });

        public void SetState(
            IIdentifier stateTypeId,
            IIdentifier stateId,
            string value) => SetStates(
                stateTypeId,
                new[] { Tuple.Create(stateId, (object)value) });

        public void SetStates(
            IIdentifier stateTypeId,
            IEnumerable<Tuple<IIdentifier, object>> statePairing)
        {
            var changedStates = new Dictionary<IIdentifier, Tuple<object, object>>();

            if (!_state.TryGetValue(
                stateTypeId,
                out var states))
            {
                states = new Dictionary<IIdentifier, object>();
                _state[stateTypeId] = states;
            }

            foreach (var pairing in statePairing)
            {
                bool changed;
                if (!states.TryGetValue(
                    pairing.Item1,
                    out var existingValue))
                {
                    changed = true;
                }
                else
                {
                    changed = !Equals(existingValue, pairing.Item2);
                }

                if (!changed)
                {
                    continue;
                }

                states[pairing.Item1] = pairing.Item2;
                changedStates[pairing.Item1] = Tuple.Create(
                    pairing.Item2,
                    existingValue);
            }

            if (changedStates.Count > 0)
            {
                StateChanged?.Invoke(
                    this,
                    new StateChangedEventArgs(
                        stateTypeId,
                        changedStates));
            }
        }
    }
}
