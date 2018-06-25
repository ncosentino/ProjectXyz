using Autofac;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
