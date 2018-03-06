using Autofac;
using ProjectXyz.Game.Core.Systems;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class SystemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatUpdaterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectManagerSystem>()
                .AsImplementedInterfaces();
            ////.SingleInstance();
        }
    }
}