using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExpiryEnchantmentTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
