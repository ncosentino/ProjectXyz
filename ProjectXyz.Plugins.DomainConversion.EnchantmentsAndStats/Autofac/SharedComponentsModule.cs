using Autofac;

namespace ProjectXyz.Plugins.Enchantments.Stats.StatExpressions.Autofac
{
    public sealed class SharedComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<EnchantmentExpressionInterceptorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
