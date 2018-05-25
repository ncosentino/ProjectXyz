using Autofac;

namespace Examples.Modules.GameObjects
{
    public sealed class GameObjectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}