using System.Collections.Generic;
using Autofac;
using ProjectXyz.Game.Core.Engine;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Systems;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class GameEngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new GameEngine(
                    c.Resolve<IGameObjectManager>(),
                    c.Resolve<IEnumerable<ISystem>>(),
                    c.Resolve<IEnumerable<ISystemUpdateComponentCreator>>()))
                .AsImplementedInterfaces();
            ////.SingleInstance();
        }
    }
}