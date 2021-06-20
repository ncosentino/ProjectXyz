using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class GameModule : SingleRegistrationModule
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