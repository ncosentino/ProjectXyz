using Autofac;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Autofac
{
    public sealed class SharedComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<DurationTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedTimeTriggerSourceMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
