using Autofac;

namespace Examples.Modules.Mapping
{
    public sealed class GameObjectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<MapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}