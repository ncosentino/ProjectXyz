using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.GameObjects
{
    public sealed class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}