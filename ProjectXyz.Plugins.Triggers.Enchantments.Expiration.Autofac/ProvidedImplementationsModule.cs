using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
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
