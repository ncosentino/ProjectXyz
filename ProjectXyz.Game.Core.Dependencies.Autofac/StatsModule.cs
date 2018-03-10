using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Shared.Framework;

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
                .Register(c => new ContextConverter(
                    c.Resolve<IEnchantmentCalculatorContextFactory>(),
                    new Interval<double>(0)))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatManagerFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}