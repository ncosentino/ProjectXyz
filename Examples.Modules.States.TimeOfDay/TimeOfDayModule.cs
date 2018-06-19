using Autofac;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TimeOfDaySystem>()
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