using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.States.Autofac
{
    public sealed class StatesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var states = new Dictionary<IIdentifier, IStateContext>();
                    var stateContextProvider = new StateContextProvider(states);
                    return stateContextProvider;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
