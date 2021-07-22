using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default.Autofac
{
    public sealed class StatesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StateManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ContextToExpressionInterceptorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var stateIdToTermMapping = c
                        .Resolve<IEnumerable<IDiscoverableStateIdToTermRepository>>()
                        .SelectMany(x => x.GetStateIdToTermMappings())
                        .ToDictionary(
                            x => x.StateIdentifier,
                            x => (IReadOnlyDictionary<IIdentifier, string>)x.TermMapping);
                    var stateValueInjector = new StateValueInjector(
                        c.Resolve<Lazy<IReadOnlyStateManager>>(),
                        stateIdToTermMapping);
                    return stateValueInjector;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var stateExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                        c.Resolve<IStateValueInjector>(),
                        1);
                    return stateExpressionInterceptorFactory;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}