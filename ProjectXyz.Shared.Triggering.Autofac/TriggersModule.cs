using Autofac;

namespace ProjectXyz.Shared.Triggering.Autofac
{
    public sealed class TriggersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TriggerMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}