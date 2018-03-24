using Autofac;
using ProjectXyz.Game.Core.GameObjects;
using ProjectXyz.Game.Core.GameObjects.Actors;
using ProjectXyz.Game.Core.GameObjects.Items;

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
            builder
                .RegisterType<ActorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}