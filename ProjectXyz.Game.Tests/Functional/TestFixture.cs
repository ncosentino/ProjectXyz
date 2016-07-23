using System.Collections.Generic;
using System.Linq;
using Jace;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Core.Enchantments.Expiration;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Core.Triggering.Triggers.Duration;
using ProjectXyz.Application.Core.Triggering.Triggers.Elapsed;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments.Expiration;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Interface.Triggering;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;

namespace ProjectXyz.Game.Tests.Functional
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            var statDefinitionIdToTermMapping = testData.StatDefinitionIdToTermMapping;
            var statDefinitionIdToCalculationMapping = testData.StatDefinitionIdToCalculationMapping;

            var jaceCalculationEngine = new CalculationEngine();
            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new GenericExpressionEvaluator(jaceCalculationEngine.Calculate), true);

            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(new IStatCalculationNodeFactory[]
            {
                statCalculationValueNodeFactory,
                statCalculationExpressionNodeFactory
            });

            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();

            StatBoundsExpressionInterceptor = new StatBoundsExpressionInterceptor(testData.StatBounds);
            var statBoundsExpressionInterceptor = StatBoundsExpressionInterceptor;

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statBoundsExpressionInterceptor,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);

            var statCalculator = new StatCalculator(statCalculationNodeCreator);

            var enchantmentExpressionInterceptorConverter = new EnchantmentExpressionInterceptorConverter();

            var stateValueInjector = new StateValueInjector(testData.StateIdToTermMapping);
            var stateEnchantmentExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping);

            var valueMappingExpressionInterceptorFactory = new ValueMappingExpressionInterceptorFactory();

            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                enchantmentExpressionInterceptorConverter);

            var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(testData.ContextToValueMapping);

            var contextToInterceptorsConverter = new ContextToInterceptorsConverter(
                stateEnchantmentExpressionInterceptorFactory,
                valueMappingExpressionInterceptorFactory,
                contextToTermValueMappingConverter);

            EnchantmentCalculator = new EnchantmentCalculator(
                enchantmentStatCalculator,
                contextToInterceptorsConverter);

            EnchantmentApplier = new EnchantmentApplier(EnchantmentCalculator);

            ElapsedTimeTriggerSourceMechanic = new ElapsedTimeTriggerSourceMechanic();

            TriggerMechanicRegistrar = new TriggerMechanicRegistrar(ElapsedTimeTriggerSourceMechanic.AsArray());

            var durationTriggerMechanicFactory = new DurationTriggerMechanicFactory();
            var expiryTriggerMechanicFactory = new ExpiryTriggerMechanicFactory(durationTriggerMechanicFactory);
            ActiveEnchantmentManager = new ActiveEnchantmentManager(
                expiryTriggerMechanicFactory,
                TriggerMechanicRegistrar);
        }
        #endregion

        #region Properties
        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public ElapsedTimeTriggerSourceMechanic ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManager ActiveEnchantmentManager { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStatExpressionInterceptor StatBoundsExpressionInterceptor { get; }
        #endregion
    }

    public sealed class EnchantmentFactory
    {
        public IEnchantment CreateExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority,
            params IExpiryComponent[] expiry)
        {
            IEnumerable<IComponent> components = new EnchantmentExpressionComponent(
                calculationPriority,
                expression)
                .Yield();

            if (expiry != null)
            {
                components = components.Concat(expiry);
            }

            return new Enchantment(
                statDefinitionId,
                components);
        }
    }
}