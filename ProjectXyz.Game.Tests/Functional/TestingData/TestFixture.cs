using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Jace;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Bounded;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Stats.Core.Calculations;
using ProjectXyz.Application.Stats.Interface.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData.States;
using ProjectXyz.Game.Tests.Functional.TestingData.Stats;
using ProjectXyz.Plugins.Triggers.Elapsed;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules = moduleDiscoverer.Discover("*.Tests.dll")
                .Concat(moduleDiscoverer
                .Discover("*.Dependencies.Autofac.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            EnchantmentCalculator = dependencyContainer.Resolve<IEnchantmentCalculator>();
            EnchantmentApplier = dependencyContainer.Resolve<IEnchantmentApplier>();
            ElapsedTimeTriggerSourceMechanic = dependencyContainer.Resolve<IElapsedTimeTriggerSourceMechanicRegistrar>();
            TriggerMechanicRegistrar = dependencyContainer.Resolve<ITriggerMechanicRegistrar>();
            ActiveEnchantmentManager = dependencyContainer.Resolve<IActiveEnchantmentManager>();
            StateContextProvider = dependencyContainer.Resolve<IStateContextProvider>();
        }
        #endregion

        #region Properties
        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public IElapsedTimeTriggerSourceMechanicRegistrar ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManager ActiveEnchantmentManager { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }
        #endregion
    }
}