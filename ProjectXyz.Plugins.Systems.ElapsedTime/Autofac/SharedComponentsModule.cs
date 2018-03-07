using Autofac;

namespace ProjectXyz.Plugins.Systems.ElapsedTime.Autofac
{
    public sealed class SharedComponentsModule : Module
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
        }
    }
}
