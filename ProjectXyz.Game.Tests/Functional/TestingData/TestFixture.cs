using System;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.ElapsedTime;

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
        }
        #endregion

        #region Properties
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