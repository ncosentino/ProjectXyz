using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Plugins.Features.Stats.Calculations;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Autofac
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            RegisterCalculationsImplementations(builder);

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
            builder
                .RegisterType<EnchantmentTriggerMechanicFactoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterCalculationsImplementations(ContainerBuilder builder)
        {
            builder
                .RegisterType<CalculationPriorityFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentCalculator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<OverrideBaseStatComponentHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var contextToInterceptorsConverter = new ContextToInterceptorsConverter();
                    var contextToExpressionInterceptorConverters =
                        c.Resolve<IEnumerable<IContextToExpressionInterceptorConverter>>();
                    foreach (var contextToExpressionInterceptorConverter in contextToExpressionInterceptorConverters)
                    {
                        contextToInterceptorsConverter.Register(contextToExpressionInterceptorConverter);
                    }

                    return contextToInterceptorsConverter;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    return new EnchantmentCalculatorContextFactory(new IComponent[]
                    {
                        // FIXME: we used to have state stuff in here... but
                        // that was it. is this component concept dead here?
                    });
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}