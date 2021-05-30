using Autofac;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Testing;
using ProjectXyz.Tests.Functional.TestingData.Enchantments;

namespace ProjectXyz.Tests.Functional.TestingData
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            LifeTimeScope = new TestLifeTimeScopeFactory().CreateScope();

            EnchantmentCalculator = LifeTimeScope.Resolve<IEnchantmentCalculator>();
            EnchantmentApplier = LifeTimeScope.Resolve<IEnchantmentApplier>();
            ElapsedTurnsTriggerSourceMechanic = LifeTimeScope.Resolve<IElapsedTurnsTriggerSourceMechanicRegistrar>();
            TriggerMechanicRegistrar = LifeTimeScope.Resolve<ITriggerMechanicRegistrar>();
            ActiveEnchantmentManagerFactory = LifeTimeScope.Resolve<IActiveEnchantmentManagerFactory>();
            HasEnchantmentsBehaviorFactory = LifeTimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            HasMutableStatsBehaviorFactory = LifeTimeScope.Resolve<IHasMutableStatsBehaviorFactory>();
            StateContextProvider = LifeTimeScope.Resolve<IStateContextProvider>();
            StatManagerFactory = LifeTimeScope.Resolve<IStatManagerFactory>();
            ContextConverter = LifeTimeScope.Resolve<IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>>();
            ActorFactory = LifeTimeScope.Resolve<IActorFactory>();
            ItemFactory = LifeTimeScope.Resolve<IItemFactory>();
            StatDefinitionToTermConverter = LifeTimeScope.Resolve<IStatDefinitionToTermConverter>();
            StatCalculationService = LifeTimeScope.Resolve<IStatCalculationService>();
            CalculationPriorityFactory = LifeTimeScope.Resolve<ICalculationPriorityFactory>();
            ItemSetManager = LifeTimeScope.Resolve<IItemSetManager>();
            SkillFactory = LifeTimeScope.Resolve<ISkillFactory>();
            ComponentsForTargetComponentFactory = LifeTimeScope.Resolve<IComponentsForTargetComponentFactory>();
            GameObjectFactory = LifeTimeScope.Resolve<IGameObjectFactory>();
            EnchantmentFactory = new ExpressionEnchantmentFactory(LifeTimeScope.Resolve<IEnchantmentFactory>());
        }
        #endregion

        #region Properties
        public ExpressionEnchantmentFactory EnchantmentFactory { get; }

        public IStatCalculationService StatCalculationService { get; }

        public IStatDefinitionToTermConverter StatDefinitionToTermConverter { get; }

        public IItemFactory ItemFactory { get; }

        public IActorFactory ActorFactory { get; }

        public ISkillFactory SkillFactory { get; }

        public IGameObjectFactory GameObjectFactory{ get; }

        public IStatManagerFactory StatManagerFactory { get; }

        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public IElapsedTurnsTriggerSourceMechanicRegistrar ElapsedTurnsTriggerSourceMechanic { get; }

        public IActiveEnchantmentManagerFactory ActiveEnchantmentManagerFactory { get; }

        public IHasMutableStatsBehaviorFactory HasMutableStatsBehaviorFactory { get; }

        public IHasEnchantmentsBehaviorFactory HasEnchantmentsBehaviorFactory { get; }

        public IComponentsForTargetComponentFactory ComponentsForTargetComponentFactory { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> ContextConverter { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }

        public ICalculationPriorityFactory CalculationPriorityFactory { get; }

        public IItemSetManager ItemSetManager { get; }

        public ILifetimeScope LifeTimeScope { get; }
        #endregion
    }
}