using Autofac;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Autofac
{
    public sealed class InternalDependenciesModule : Module
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
