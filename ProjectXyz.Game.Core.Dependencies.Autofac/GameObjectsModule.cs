using Autofac;
using ProjectXyz.Game.Core.GameObjects;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class GameObjectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GameObjectManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}