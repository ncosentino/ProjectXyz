using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Default.Autofac
{
    public sealed class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectHierarchy>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}