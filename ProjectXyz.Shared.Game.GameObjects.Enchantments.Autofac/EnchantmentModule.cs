using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Autofac
{
    public sealed class EnchantmentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<GeneratorContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<BaseEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new StatCalculatorWrapper(
                    c.Resolve<IStatCalculator>(),
                    c.Resolve<IEnumerable<IStatExpressionInterceptor>>(),
                    c.Resolve<IEnumerable<IEnchantmentExpressionInterceptorConverter>>()))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActiveEnchantmentManagerFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}