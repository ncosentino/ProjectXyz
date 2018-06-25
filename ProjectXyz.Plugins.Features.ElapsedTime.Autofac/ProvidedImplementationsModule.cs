using Autofac;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ElapsedTimeComponentCreator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedTimeTriggerMechanicSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
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
