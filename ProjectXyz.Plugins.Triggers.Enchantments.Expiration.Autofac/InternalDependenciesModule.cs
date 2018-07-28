using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Autofac
{
    public sealed class InternalDependenciesModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ExpiryTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
