using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Enchantments.Stats.StatExpressions.Autofac
{
    public sealed class SharedComponentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantmentExpressionInterceptorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
