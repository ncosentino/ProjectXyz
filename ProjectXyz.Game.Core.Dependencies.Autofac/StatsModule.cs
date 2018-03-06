using Autofac;
using ProjectXyz.Game.Core.Stats;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class StatsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatUpdater>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
        }
    }
}