using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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
