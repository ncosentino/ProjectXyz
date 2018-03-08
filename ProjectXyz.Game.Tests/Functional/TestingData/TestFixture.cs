using System.Linq;
using Autofac;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Stats;
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
            DependencyContainer = dependencyContainerBuilder.Create(modules);

            EnchantmentCalculator = DependencyContainer.Resolve<IEnchantmentCalculator>();
            EnchantmentApplier = DependencyContainer.Resolve<IEnchantmentApplier>();
            ElapsedTimeTriggerSourceMechanic = DependencyContainer.Resolve<IElapsedTimeTriggerSourceMechanicRegistrar>();
            TriggerMechanicRegistrar = DependencyContainer.Resolve<ITriggerMechanicRegistrar>();
            ActiveEnchantmentManagerFactory = DependencyContainer.Resolve<IActiveEnchantmentManagerFactory>();
            StateContextProvider = DependencyContainer.Resolve<IStateContextProvider>();
            StatManagerFactory = DependencyContainer.Resolve<IStatManagerFactory>();
        }
        #endregion

        #region Properties
        public IStatManagerFactory StatManagerFactory { get; }

        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public IElapsedTimeTriggerSourceMechanicRegistrar ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManagerFactory ActiveEnchantmentManagerFactory { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }

        private IContainer DependencyContainer { get; }
        #endregion
    }
}