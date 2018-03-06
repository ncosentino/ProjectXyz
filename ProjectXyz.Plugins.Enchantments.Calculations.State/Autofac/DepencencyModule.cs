using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.States;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State.Autofac
{
    public sealed class DepencencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var stateIdToTermMapping = c.Resolve<IStateIdToTermRepository>()
                        .GetStateIdToTermMappings()
                        .ToDictionary(
                            x => x.StateIdentifier,
                            x => (IReadOnlyDictionary<IIdentifier, string>)x.TermMapping);
                    var stateValueInjector = new StateValueInjector(stateIdToTermMapping);
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
