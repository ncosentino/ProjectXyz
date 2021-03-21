using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Autofac
{
    public sealed class TurnBasedModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TurnBasedComponentCreator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TurnBasedManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedTurnsTriggerMechanicSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DurationTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedTurnsTriggerSourceMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
