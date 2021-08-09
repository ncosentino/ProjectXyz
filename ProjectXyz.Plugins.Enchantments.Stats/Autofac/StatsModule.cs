using Autofac;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Enchantments.Stats.Autofac
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ContextConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatManagerFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatCalculationContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => c
                    .Resolve<IStatManagerFactory>()
                    .Create(c.Resolve<IMutableStatsProvider>()))
                .AsImplementedInterfaces();
            ////.SingleInstance(); // *NOT* single instance
        }
    }
}