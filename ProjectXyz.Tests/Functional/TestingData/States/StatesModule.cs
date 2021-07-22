using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default;
using ProjectXyz.Plugins.Features.TimeOfDay;

namespace ProjectXyz.Tests.Functional.TestingData.States
{
    public sealed class StatesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class StateIdToTermRepo : IDiscoverableStateIdToTermRepository
        {
            private readonly StateInfo _stateInfo;
            private readonly ITimeOfDayIdentifiers _timeOfDayIdentifiers;

            public StateIdToTermRepo(ITimeOfDayIdentifiers timeOfDayIdentifiers)
            {
                _stateInfo = new StateInfo();
                _timeOfDayIdentifiers = timeOfDayIdentifiers;
            }

            public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
            {
                yield return new StateIdToTermMapping(
                    _timeOfDayIdentifiers.TimeOfDayStateTypeId,
                    new TermMapping(new Dictionary<IIdentifier, string>()
                    {
                        { _timeOfDayIdentifiers.CyclePercentStateId, "TimeOfDayCycle" },
                    }));
            }
        }
    }
}
