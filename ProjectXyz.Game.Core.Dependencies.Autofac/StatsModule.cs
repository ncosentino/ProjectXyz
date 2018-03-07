using Autofac;
using ProjectXyz.Framework.Shared;
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
            builder
                .Register(x => new ContextConverter(new Interval<double>(0)))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatManagerFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}