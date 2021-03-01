using Autofac;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Game.Tests.Functional.TestingData.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.ElapsedTime;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Testing;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            LifeTimeScope = new TestLifeTimeScopeFactory().CreateScope();

            EnchantmentCalculator = LifeTimeScope.Resolve<IEnchantmentCalculator>();
            EnchantmentApplier = LifeTimeScope.Resolve<IEnchantmentApplier>();
            ElapsedTimeTriggerSourceMechanic = LifeTimeScope.Resolve<IElapsedTimeTriggerSourceMechanicRegistrar>();
            TriggerMechanicRegistrar = LifeTimeScope.Resolve<ITriggerMechanicRegistrar>();
            ActiveEnchantmentManagerFactory = LifeTimeScope.Resolve<IActiveEnchantmentManagerFactory>();
            HasEnchantmentsBehaviorFactory = LifeTimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            StateContextProvider = LifeTimeScope.Resolve<IStateContextProvider>();
            StatManagerFactory = LifeTimeScope.Resolve<IStatManagerFactory>();
            ContextConverter = LifeTimeScope.Resolve<IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>>();
            ActorFactory = LifeTimeScope.Resolve<IActorFactory>();
            ItemFactory = LifeTimeScope.Resolve<IItemFactory>();
            StatDefinitionToTermConverter = LifeTimeScope.Resolve<IStatDefinitionToTermConverter>();
            StatCalculationService = LifeTimeScope.Resolve<IStatCalculationService>();
            CalculationPriorityFactory = LifeTimeScope.Resolve<ICalculationPriorityFactory>();
            ItemSetManager = LifeTimeScope.Resolve<IItemSetManager>();
            EnchantmentFactory = new ExpressionEnchantmentFactory(LifeTimeScope.Resolve<IEnchantmentFactory>());
        }
        #endregion

        #region Properties
        public ExpressionEnchantmentFactory EnchantmentFactory { get; }

        public IStatCalculationService StatCalculationService { get; }

        public IStatDefinitionToTermConverter StatDefinitionToTermConverter { get; }

        public IItemFactory ItemFactory { get; }

        public IActorFactory ActorFactory { get; }

        public IStatManagerFactory StatManagerFactory { get; }

        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public IElapsedTimeTriggerSourceMechanicRegistrar ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManagerFactory ActiveEnchantmentManagerFactory { get; }

        public IHasEnchantmentsBehaviorFactory HasEnchantmentsBehaviorFactory { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> ContextConverter { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }

        public ICalculationPriorityFactory CalculationPriorityFactory { get; }

        public IItemSetManager ItemSetManager { get; }

        private ILifetimeScope LifeTimeScope { get; }
        #endregion
    }
}