using Autofac;

using Examples.Modules.GameObjects;

using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.Mapping
{
    public sealed class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapGameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}