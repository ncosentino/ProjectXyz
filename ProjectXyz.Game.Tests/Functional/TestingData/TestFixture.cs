using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jace;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Bounded;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Stats.Core.Calculations;
using ProjectXyz.Application.Stats.Interface.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData.States;
using ProjectXyz.Game.Tests.Functional.TestingData.Stats;
using ProjectXyz.Plugins.Core;
using ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers;
using ProjectXyz.Plugins.Triggers.Elapsed;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestFixture
    {
        #region Constructors
        public TestFixture(TestData testData)
        {
            var pluginArgs = new PluginArgs(new IComponent[]
            {
                new GenericComponent<IValueMapperRepository>(new ValueMapperRepository(testData.UnitInterval)),
                new GenericComponent<IStatDefinitionIdToBoundsMappingRepository>(new StatDefinitionIdToBoundsMappingRepository(testData.Stats)),
            });
            var pluginTypes = new[]
            {
                typeof(StatsPlugin),
                typeof(StatesPlugin),
                typeof(Plugins.Triggers.Elapsed.Plugin),
                typeof(Plugins.DomainConversion.EnchantmentsAndStats.Plugin),
                typeof(Plugins.DomainConversion.EnchantmentsAndTriggers.Plugin),
                typeof(Plugins.Stats.Calculations.Bounded.Plugin),
                typeof(Plugins.Enchantments.StatToTerm.Plugin),
                typeof(Plugins.Enchantments.Calculations.Expressions.Plugin),
                typeof(Plugins.Enchantments.Calculations.State.Plugin),

            };
            var pluginLoader = new PluginLoader();
            var pluginLoadResult = pluginLoader.LoadPlugins(
                pluginArgs,
                pluginTypes);

            var statDefinitionIdToTermMapping = pluginLoadResult
                .Components
                .TakeTypes<IComponent<IStatDefinitionToTermMappingRepository>>()
                .Single()
                .Value
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(x => x.StateDefinitionId, x => x.Term);

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

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);

            var statCalculator = new StatCalculator(statCalculationNodeCreator);

            var enchantmentExpressionInterceptorConverters = pluginLoadResult
                .Components
                .TakeTypes<IComponent<IEnchantmentExpressionInterceptorConverter>>()
                .Select(x => x.Value);
            var statExpressionInterceptors = pluginLoadResult
                .Components
                .TakeTypes<IComponent<IStatExpressionInterceptor>>()
                .Select(x => x.Value);
            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                statExpressionInterceptors,
                enchantmentExpressionInterceptorConverters);

            var contextToInterceptorsConverter = new ContextToInterceptorsConverter();

            foreach (var contextToExpressionInterceptorConverter in pluginLoadResult
                .Components
                .TakeTypes<IComponent<IContextToExpressionInterceptorConverter>>()
                .Select(x => x.Value))
            {
                contextToInterceptorsConverter.Register(contextToExpressionInterceptorConverter);
            }

            EnchantmentCalculator = new EnchantmentCalculator(
                enchantmentStatCalculator,
                contextToInterceptorsConverter);

            EnchantmentApplier = new EnchantmentApplier(EnchantmentCalculator);

            var triggerSourceMechanics = pluginLoadResult
                .Components
                .TakeTypes<IComponent<ITriggerSourceMechanic>>()
                .Select(x => x.Value);
            var triggerSourceMechanicRegistrars = pluginLoadResult
                .Components
                .TakeTypes<IComponent<ITriggerMechanicRegistrar>>()
                .Select(x => x.Value);

            ElapsedTimeTriggerSourceMechanic = (ElapsedTimeTriggerSourceMechanic)triggerSourceMechanics.Single();

            TriggerMechanicRegistrar = new TriggerMechanicRegistrar(triggerSourceMechanicRegistrars);

            var enchantmentTriggerMechanicRegistrars = pluginLoadResult
                .Components
                .TakeTypes<IComponent<IEnchantmentTriggerMechanicRegistrar>>()
                .Select(x => x.Value);
            ActiveEnchantmentManager = new ActiveEnchantmentManager(
                TriggerMechanicRegistrar,
                enchantmentTriggerMechanicRegistrars);

            StateContextProvider = pluginLoadResult
                .Components
                .TakeTypes<IComponent<IStateContextProvider>>()
                .Single()
                .Value;
        }
        #endregion

        #region Properties
        public ITriggerMechanicRegistrar TriggerMechanicRegistrar { get; }

        public ElapsedTimeTriggerSourceMechanic ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManager ActiveEnchantmentManager { get; }

        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public IStateContextProvider StateContextProvider { get; }
        #endregion
    }


    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        private readonly IInterval _unitInterval;

        public ValueMapperRepository(IInterval unitInterval)
        {
            _unitInterval = unitInterval;
        }

        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("INTERVAL", context.Elapsed.Divide(_unitInterval));
        }
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