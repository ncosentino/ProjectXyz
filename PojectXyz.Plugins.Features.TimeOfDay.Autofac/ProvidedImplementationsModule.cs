using Autofac;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TimeOfDaySystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayGenerationContextAttributeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}