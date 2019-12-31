using System.Collections;
using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;
using ProjectXyz.Plugins.Features.StateEnchantments.Shared;

namespace ProjectXyz.Game.Tests.Functional.TestingData.States
{
    public sealed class StatesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StateContextProv>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

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
                yield return new StateIdToTermMapping(
                    _stateInfo.States.TimeOfDay,
                    new TermMapping(new Dictionary<IIdentifier, string>()
                    {
                        { _stateInfo.TimeOfDay.Day, "TOD_DAY" },
                        { _stateInfo.TimeOfDay.Night, "TOD_NIGHT" },
                    }));
            }
        }
    }
}
