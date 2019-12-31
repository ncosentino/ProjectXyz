using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm.Autofac
{
    public sealed class SharedComponentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ContextToExpressionInterceptorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
