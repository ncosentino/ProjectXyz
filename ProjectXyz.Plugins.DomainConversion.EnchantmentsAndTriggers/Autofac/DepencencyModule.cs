using Autofac;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration.Autofac
{
    public sealed class DepencencyModule : Module
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
