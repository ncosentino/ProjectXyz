using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Application.States.Core;

namespace ProjectXyz.Application.Core.Dependencies.Autofac
{
    public sealed class StatesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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
