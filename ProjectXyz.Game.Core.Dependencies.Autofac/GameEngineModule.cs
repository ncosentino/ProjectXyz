using Autofac;
using ProjectXyz.Game.Core.Engine;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class GameEngineModule : Module
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