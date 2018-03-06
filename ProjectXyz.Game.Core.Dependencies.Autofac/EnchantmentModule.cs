using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Game.Core.Enchantments;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class EnchantmentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new StatCalculatorWrapper(
                    c.Resolve<IStatCalculator>(),
                    c.Resolve<IEnumerable<IStatExpressionInterceptor>>(),
                    c.Resolve<IEnumerable<IEnchantmentExpressionInterceptorConverter>>()))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActiveEnchantmentManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}