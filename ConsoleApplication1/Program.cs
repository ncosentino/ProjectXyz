using System;
using System.Collections.Generic;
using Jace;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Application.Stats.Core.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Stats;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var enchantmentTriggerMechanicRegistrars = new IEnchantmentTriggerMechanicRegistrar[0];
            var triggerMechanicRegistrars = new ITriggerMechanicRegistrar[0];
            var statDefinitionIdToTermMapping = new Dictionary<IIdentifier, string>();
            var statDefinitionIdToCalculationMapping = new Dictionary<IIdentifier, string>();
            var statExpressionInterceptors = new IStatExpressionInterceptor[0];
            var enchantmentExpressionInterceptorConverters = new IEnchantmentExpressionInterceptorConverter[0];
            var enchantmentCalculatorContextFactoryComponents = new IComponent[0];

            var mutableStatsProvider = new MutableStatsProvider();
            var triggerMechanicRegistrar = new TriggerMechanicRegistrar(triggerMechanicRegistrars);
            var activeEnchantmentManager = new ActiveEnchantmentManager(
                triggerMechanicRegistrar,
                enchantmentTriggerMechanicRegistrars);
            var jaceCalculationEngine = new CalculationEngine();
            Func<string, double> calculateCallback = jaceCalculationEngine.Calculate;
            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new GenericExpressionEvaluator(calculateCallback), true);
            var statCalculationNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();
            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);
            var statCalculator = new StatCalculator(statCalculationNodeCreator);
            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                statExpressionInterceptors,
                enchantmentExpressionInterceptorConverters);
            var contextToInterceptorsConverter = new ContextToInterceptorsConverter();
            var enchantmentCalculator = new EnchantmentCalculator(
                enchantmentStatCalculator,
                contextToInterceptorsConverter);
            var enchantmentApplier = new EnchantmentApplier(enchantmentCalculator);
            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory(enchantmentCalculatorContextFactoryComponents);
            var statUpdater = new StatUpdater(
                mutableStatsProvider,
                activeEnchantmentManager,
                enchantmentApplier,
                enchantmentCalculatorContextFactory);
            var buffable = new Buffable(activeEnchantmentManager);

            Console.ReadLine();
        }
    }

    public sealed class Actor
    {
        public Actor()
        {
        }
    }

    public interface IBehavior
    {
        
    }

    public interface IHasEnchantments : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }

    public interface IBuffable : IBehavior
    {
        void AddEnchantments(IEnumerable<IEnchantment> enchantments);

        void RemoveEnchantments(IEnumerable<IEnchantment> enchantments);
    }

    public sealed class HasEnchantments : IHasEnchantments
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public HasEnchantments(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public IReadOnlyCollection<IEnchantment> Enchantments => _activeEnchantmentManager.Enchantments;
    }

    public sealed class Buffable : IBuffable
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public Buffable(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Add(enchantments);
        }

        public void RemoveEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Remove(enchantments);
        }
    }
}