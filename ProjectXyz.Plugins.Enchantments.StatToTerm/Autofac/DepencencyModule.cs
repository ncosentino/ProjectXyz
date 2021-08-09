using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var statToTermExpressionInterceptorFactory = new StatToTermExpressionInterceptorFactory(
                        c.Resolve<IStatDefinitionToTermConverter>(),
                        0);
                    return statToTermExpressionInterceptorFactory;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
