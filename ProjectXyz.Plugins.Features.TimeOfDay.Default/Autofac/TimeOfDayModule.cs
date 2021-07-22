using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Default.Autofac
{
    public sealed class TimeOfDayModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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
                .RegisterType<TimeOfDayIdentifiers>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ITimeOfDayIdentifiers))
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayConfiguration>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ITimeOfDayConfiguration))
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayConverter>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ITimeOfDayConverter))
                .SingleInstance();
        }
    }
}