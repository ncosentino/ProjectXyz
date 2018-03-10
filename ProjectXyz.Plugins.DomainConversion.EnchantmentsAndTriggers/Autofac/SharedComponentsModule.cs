using Autofac;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration.Autofac
{
    public sealed class SharedComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<EnchantmentExpiryTriggerMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
