using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.TurnBased.Default.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Autofac
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
                .RegisterType<TurnInfoFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActionInfoFactory>()
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
                .RegisterType<ElapsedActionsTriggerMechanicSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DurationInTurnsTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DurationInActionsTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedTurnsTriggerSourceMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapsedActionsTriggerSourceMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
