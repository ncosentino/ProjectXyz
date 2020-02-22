using System;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Stats;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Tests.Functional.TestingData.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.ElapsedTime;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules = moduleDiscoverer
                .Discover(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !x.GetType().FullName.Equals("ProjectXyz.Game.Core.Dependencies.Autofac.PluginModule"))
                .OrderBy(x => x.GetType().FullName)
                .ToArray();
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            DependencyContainer = dependencyContainerBuilder.Create(modules);

            EnchantmentCalculator = DependencyContainer.Resolve<IEnchantmentCalculator>();
            EnchantmentApplier = DependencyContainer.Resolve<IEnchantmentApplier>();
            ElapsedTimeTriggerSourceMechanic = DependencyContainer.Resolve<IElapsedTimeTriggerSourceMechanicRegistrar>();
            TriggerMechanicRegistrar = DependencyContainer.Resolve<ITriggerMechanicRegistrar>();
            ActiveEnchantmentManagerFactory = DependencyContainer.Resolve<IActiveEnchantmentManagerFactory>();
            StateContextProvider = DependencyContainer.Resolve<IStateContextProvider>();
            StatManagerFactory = DependencyContainer.Resolve<IStatManagerFactory>();
            ContextConverter = DependencyContainer.Resolve<IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>>();
            ActorFactory = DependencyContainer.Resolve<IActorFactory>();
            ItemFactory = DependencyContainer.Resolve<IItemFactory>();
            StatDefinitionToTermConverter = DependencyContainer.Resolve<IStatDefinitionToTermConverter>();
            StatCalculationService = DependencyContainer.Resolve<IStatCalculationService>();
            EnchantmentFactory = new EnchantmentFactory(DependencyContainer.Resolve<IBehaviorCollectionFactory>());
        }
        #endregion

        #region Properties
        public EnchantmentFactory EnchantmentFactory { get; }

        public IStatCalculationService StatCalculationService { get; }

        public IStatDefinitionToTermConverter StatDefinitionToTermConverter { get; }

        public IItemFactory ItemFactory { get; }

        public IActorFactory ActorFactory { get; }

        public IStatManagerFactory StatManagerFactory { get; }

        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public IElapsedTimeTriggerSourceMechanicRegistrar ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManagerFactory ActiveEnchantmentManagerFactory { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> ContextConverter { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }

        private IContainer DependencyContainer { get; }
        #endregion
    }
}