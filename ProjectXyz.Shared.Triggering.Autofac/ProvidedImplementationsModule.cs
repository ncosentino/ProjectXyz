using Autofac;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Shared.Triggering.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TriggerMechanicRegistrar>()
                .As<ITriggerMechanicRegistrarFacade>() // specifically the facade to avoid circular dependencies
                .SingleInstance();
        }
    }
}