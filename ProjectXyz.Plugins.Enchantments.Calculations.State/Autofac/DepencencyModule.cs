using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var stateIdToTermMapping = c
                        .Resolve<IEnumerable<IStateIdToTermRepository>>()
                        .SelectMany(x => x.GetStateIdToTermMappings())
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
