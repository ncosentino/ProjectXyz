using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Core.Engine;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class GameEngineModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GameEngine>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}