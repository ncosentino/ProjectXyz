using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Application.States.Core
{
    public sealed class StateContextProvider : IStateContextProvider
    {
        #region Fields
        private readonly IReadOnlyDictionary<IIdentifier, IStateContext> _states;
        #endregion

        #region Constructors
        public StateContextProvider(IEnumerable<KeyValuePair<IIdentifier, IStateContext>> states)
            : this(states.ToDictionary())
        {
        }

        public StateContextProvider(IReadOnlyDictionary<IIdentifier, IStateContext> states)
        {
            _states = states;
        }
        #endregion

        #region Properties
        public static IStateContextProvider Empty { get; } = new StateContextProvider(Enumerable.Empty<KeyValuePair<IIdentifier, IStateContext>>());

        public int Count => _states.Count;

        public IStateContext this[IIdentifier key] => _states[key];

        public IEnumerable<IIdentifier> Keys => _states.Keys;

        public IEnumerable<IStateContext> Values => _states.Values;
        #endregion

        #region Methods
        public bool ContainsKey(IIdentifier key) => _states.ContainsKey(key);

        public bool TryGetValue(IIdentifier key, out IStateContext value) => _states.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<IIdentifier, IStateContext>> GetEnumerator() => _states.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
