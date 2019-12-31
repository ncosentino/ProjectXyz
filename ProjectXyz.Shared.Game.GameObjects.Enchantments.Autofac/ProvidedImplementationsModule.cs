using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterGenerationImplementations(builder);
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
        }

        private static void RegisterCalculationsImplementations(ContainerBuilder builder)
        {
            // TODO: should this be in the other project for shared "calculations" classes?
            builder
                .RegisterType<EnchantmentCalculator>()
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
            // TODO: should this be in the other project for shared "calculations" classes?
            builder
                .Register(c =>
                {
                    var stateContextProvider = c.Resolve<IStateContextProvider>();
                    var stateContextProviderComponent = new GenericComponent<IStateContextProvider>(stateContextProvider);
                    return new EnchantmentCalculatorContextFactory(new[]
                    {
                        stateContextProviderComponent
                    });
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterGenerationImplementations(ContainerBuilder builder)
        {
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<BaseEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<EnchantmentGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}