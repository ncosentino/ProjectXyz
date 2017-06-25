using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.States;
using ProjectXyz.Api.States.Plugins;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData.States
{
    public sealed class StatesPlugin : IStatePlugin
    {
        public StatesPlugin(IPluginArgs pluginArgs)
        {
            
        }

        public IStateIdToTermRepository StateIdToTermRepository { get; } = new StateIdToTermRepo();

        public IStateContextProvider StateContextProvider { get; } = new StateContextProv();

        private sealed class StateContextProv : IStateContextProvider
        {
            #region Fields
            private readonly IReadOnlyDictionary<IIdentifier, IStateContext> _states;
            #endregion

            #region Constructors
            public StateContextProv()
            {
                _states = new Dictionary<IIdentifier, IStateContext>();
            }
            #endregion

            #region Properties
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

        private sealed class StateIdToTermRepo : IStateIdToTermRepository
        {
            private readonly StateInfo _stateInfo;

            public StateIdToTermRepo()
            {
                _stateInfo = new StateInfo();
            }

            public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
            {
                yield return new StateIdToTermMapping()
                {
                    StateIdentifier = _stateInfo.States.TimeOfDay,
                    TermMapping = new TermMapping(new Dictionary<IIdentifier, string>()
                    {
                        { _stateInfo.TimeOfDay.Day, "TOD_DAY" },
                        { _stateInfo.TimeOfDay.Night, "TOD_NIGHT" },
                    })
                };
            }
        }

        private sealed class StateIdToTermMapping : IStateIdToTermMapping
        {
            public IIdentifier StateIdentifier { get; set; }

            public ITermMapping TermMapping { get; set; }
        }

        private sealed class TermMapping : ITermMapping
        {
            private readonly IReadOnlyDictionary<IIdentifier, string> _wrapped;

            public TermMapping(IEnumerable<KeyValuePair<IIdentifier, string>> wrapped)
            {
                _wrapped = wrapped.ToDictionary(x => x.Key, x => x.Value);
            }

            public IEnumerator<KeyValuePair<IIdentifier, string>> GetEnumerator() => _wrapped.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public int Count => _wrapped.Count;

            public bool ContainsKey(IIdentifier key) => _wrapped.ContainsKey(key);

            public bool TryGetValue(IIdentifier key, out string value) => _wrapped.TryGetValue(key, out value);

            public string this[IIdentifier key] => _wrapped[key];

            public IEnumerable<IIdentifier> Keys => _wrapped.Keys;

            public IEnumerable<string> Values => _wrapped.Values;
        }
    }
}
