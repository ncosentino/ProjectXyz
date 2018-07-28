using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.GameObjects
{
    public sealed class GameObjectsModule : SingleRegistrationModule
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