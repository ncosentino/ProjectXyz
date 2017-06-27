using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
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
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Core.Enchantments;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Systems;

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
            var statManager = new StatManager(
                enchantmentCalculator,
                mutableStatsProvider,
                new ContextConverter(new Interval<int>(0)));
            var enchantmentApplier = new EnchantmentApplier(enchantmentCalculator);
            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory(enchantmentCalculatorContextFactoryComponents);

            var hasEnchantments = new HasEnchantments(activeEnchantmentManager);
            var buffable = new Buffable(activeEnchantmentManager);
            var hasMutableStats = new HasMutableStats(mutableStatsProvider, statManager);
            var actor = new Actor(
                hasEnchantments,
                buffable,
                hasMutableStats);

            var statUpdater = new StatUpdater(
                enchantmentApplier,
                enchantmentCalculatorContextFactory);
            var behaviorFinder = new BehaviorFinder();
            var statUpdaterSystem = new StatUpdaterSystem(
                behaviorFinder,
                statUpdater);

            var elapsedTimeComponentCreator = new ElapsedTimeComponentCreator();

            var cancellationTokenSource = new CancellationTokenSource();
            var engine = new Engine(
                statUpdaterSystem.Yield(),
                elapsedTimeComponentCreator.Yield());
            engine.Start(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }

    public sealed class Engine
    {
        private readonly IReadOnlyCollection<ISystem> _systems;
        private readonly IReadOnlyCollection<ISystemUpdateComponentCreator> _systemUpdateComponentCreators;

        public Engine(
            IEnumerable<ISystem> systems,
            IEnumerable<ISystemUpdateComponentCreator> systemUpdateComponentCreators)
        {
            _systems = systems.ToArray();
            _systemUpdateComponentCreators = systemUpdateComponentCreators.ToArray();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(
                GameLoop,
                new StartArgs(cancellationToken),
                cancellationToken);
        }

        private void GameLoop(object args)
        {
            var startArgs = (StartArgs)args;
            var cancellationToken = startArgs.CancellationToken;

            var gameObjects = new List<IGameObject>();

            while (!cancellationToken.IsCancellationRequested)
            {
                var systemUpdateComponents = _systemUpdateComponentCreators.Select(x => x.CreateNext());
                var systemUpdateContext = new SystemUpdateContext(systemUpdateComponents);

                foreach (var system in _systems)
                {
                    system.Update(
                        systemUpdateContext,
                        gameObjects);
                }
            }
        }

        private sealed class StartArgs
        {
            public StartArgs(CancellationToken cancellationToken)
            {
                CancellationToken = cancellationToken;
            }

            public CancellationToken CancellationToken { get; }
        }
    }

    public interface ISystemUpdateComponentCreator
    {
        IComponent CreateNext();
    }

    public sealed class ElapsedTimeComponentCreator : ISystemUpdateComponentCreator
    {
        private DateTime? _lastUpdateTime;

        public IComponent CreateNext()
        {
            var elapsedMilliseconds = _lastUpdateTime.HasValue
                ? (DateTime.UtcNow - _lastUpdateTime.Value).TotalMilliseconds
                : 0;
            _lastUpdateTime = DateTime.UtcNow;
            var elapsedInterval = new Interval<double>(elapsedMilliseconds);

            return new GenericComponent<IElapsedTime>(new ElapsedTime(elapsedInterval));
        }
    }

    public sealed class Actor : IGameObject
    {
        public Actor(
            IHasEnchantments hasEnchantments,
            IBuffable buffable,
            IHasMutableStats hasStats)
        {
            Behaviors = new IBehavior[]
            {
                hasEnchantments,
                buffable,
                hasStats,
            };
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}