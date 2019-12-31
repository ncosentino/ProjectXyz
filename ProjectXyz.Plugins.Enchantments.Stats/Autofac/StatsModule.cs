using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Enchantments.Stats.Autofac
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new ContextConverter(
                    c.Resolve<IEnchantmentCalculatorContextFactory>(),
                    new Interval<double>(0)))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatManagerFactory>()
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