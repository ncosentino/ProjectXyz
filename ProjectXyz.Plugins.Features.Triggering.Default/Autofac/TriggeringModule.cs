using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Triggering.Default;

namespace ProjectXyz.Plugins.Features.Triggering.Default.Autofac
{
    public sealed class TriggeringModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TriggerMechanicRegistrarFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}