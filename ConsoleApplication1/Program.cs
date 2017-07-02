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
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Bounded;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Stats.Calculations.Plugins;
using ProjectXyz.Api.Stats.Plugins;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
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
using ProjectXyz.Game.Core.Engine;
using ProjectXyz.Game.Core.GameObjects;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Systems;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Core;
using ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers;
using ProjectXyz.Plugins.Interface;
using ProjectXyz.Plugins.Stats.Calculations.Bounded;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var statDefinitionIdToCalculationMapping = new Dictionary<IIdentifier, string>();
            var enchantmentCalculatorContextFactoryComponents = new IComponent[0];

            var pluginLoaderResult = LoadPlugins();
            var enchantmentExpressionInterceptorConverters = pluginLoaderResult
                .Components
                .TakeTypes<IComponent<IEnchantmentExpressionInterceptorConverter>>()
                .Select(x => x.Value);
            var statExpressionInterceptors = pluginLoaderResult
                .Components
                .TakeTypes<IComponent<IStatExpressionInterceptor>>()
                .Select(x => x.Value);
            var statDefinitionIdToTermMapping = pluginLoaderResult
                .Components
                .TakeTypes<IComponent<IStatDefinitionToTermMappingRepository>>()
                .Single()
                .Value
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(x => x.StateDefinitionId, x => x.Term);

            var contextToInterceptorsConverter = new ContextToInterceptorsConverter();
            foreach (var contextToExpressionInterceptorConverter in pluginLoaderResult
                .Components
                .TakeTypes<IComponent<IContextToExpressionInterceptorConverter>>()
                .Select(x => x.Value))
            {
                contextToInterceptorsConverter.Register(contextToExpressionInterceptorConverter);
            }

            var elapsedTimeTriggerSourceMechanic = (IElapsedTimeTriggerSourceMechanic)pluginLoaderResult
                .Components
                .Get<IComponent<ITriggerSourceMechanic>>()
                .First(x => x.Value is IElapsedTimeTriggerSourceMechanic)
                .Value;

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
            var enchantmentCalculator = new EnchantmentCalculator(
                enchantmentStatCalculator,
                contextToInterceptorsConverter);

            var enchantmentApplier = new EnchantmentApplier(enchantmentCalculator);
            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory(enchantmentCalculatorContextFactoryComponents);

            var statUpdater = new StatUpdater(
                enchantmentApplier,
                enchantmentCalculatorContextFactory);
            var behaviorFinder = new BehaviorFinder();
            var statUpdaterSystem = new StatUpdaterSystem(
                behaviorFinder,
                statUpdater);

            var elapsedTimeComponentCreator = new ElapsedTimeComponentCreator();

            var gameObjectManager = new GameObjectManager();

            var cancellationTokenSource = new CancellationTokenSource();
            var gameEngine = new GameEngine(
                gameObjectManager,
                new ISystem[]
                {
                    new GameObjectManagerSystem(gameObjectManager), 
                    statUpdaterSystem,
                    new StatPrinterSystem(),
                    new ElapsedTimeTriggerMechanicSystem(elapsedTimeTriggerSourceMechanic), 
                },
                elapsedTimeComponentCreator.Yield());
            gameEngine.Start(cancellationTokenSource.Token);

            gameObjectManager.MarkForAddition(CreateActor(pluginLoaderResult.Components));

            Console.ReadLine();
        }

        private static IPluginLoaderResult LoadPlugins()
        {
            var pluginArgs = new PluginArgs(new IComponent[]
            {
                new GenericComponent<IValueMapperRepository>(new ValueMapperRepository(new Interval<double>(1))),
                ////new GenericComponent<IStatDefinitionIdToBoundsMappingRepository>(new StatDefinitionIdToBoundsMappingRepository(testData.Stats)),
            });
            var pluginTypes = new Type[]
            {
                typeof(StatsPlugin),
                ////typeof(StatesPlugin),
                typeof(ProjectXyz.Plugins.Triggers.Elapsed.Plugin),
                typeof(ProjectXyz.Plugins.DomainConversion.EnchantmentsAndStats.Plugin),
                typeof(ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers.Plugin),
                ////typeof(ProjectXyz.Plugins.Stats.Calculations.Bounded.Plugin),
                typeof(ProjectXyz.Plugins.Enchantments.StatToTerm.Plugin),
                typeof(ProjectXyz.Plugins.Enchantments.Calculations.Expressions.Plugin),
                ////typeof(ProjectXyz.Plugins.Enchantments.Calculations.State.Plugin),

            };
            var pluginLoader = new PluginLoader();
            var pluginLoaderResult = pluginLoader.LoadPlugins(
               pluginArgs,
               pluginTypes);
            return pluginLoaderResult;
        }

        private static Actor CreateActor(IComponentCollection loadedGameComponents)
        {
            var enchantmentTriggerMechanicRegistrars = loadedGameComponents
                .TakeTypes<IComponent<IEnchantmentTriggerMechanicRegistrar>>()
                .Select(x => x.Value);
            var triggerMechanicRegistrars = loadedGameComponents
                .TakeTypes<IComponent<ITriggerMechanicRegistrar>>()
                .Select(x => x.Value);

            var mutableStatsProvider = new MutableStatsProvider();
            var triggerMechanicRegistrar = new TriggerMechanicRegistrar(triggerMechanicRegistrars);
            var activeEnchantmentManager = new ActiveEnchantmentManager(
                triggerMechanicRegistrar,
                enchantmentTriggerMechanicRegistrars);

            var hasEnchantments = new HasEnchantments(activeEnchantmentManager);
            var buffable = new Buffable(activeEnchantmentManager);
            var hasMutableStats = new HasMutableStats(mutableStatsProvider);
            var actor = new Actor(
                hasEnchantments,
                buffable,
                hasMutableStats);

            buffable.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new StringIdentifier("stat1"),
                    new IComponent[]
                    {
                        new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat1 + 1"),
                        new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
                    }),
            });


            return actor;
        }
    }

    public sealed class StatsPlugin : IStatPlugin
    {
        public StatsPlugin(IPluginArgs pluginArgs)
        {
            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IStatDefinitionToTermMappingRepository>(new StatDefinitionToTermMappingRepo()),
            });
        }

        public IComponentCollection SharedComponents { get; }

        private sealed class StatDefinitionToTermMappingRepo : IStatDefinitionToTermMappingRepository
        {
            public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
            {
                yield return new StatDefinitionToTermMapping() { StateDefinitionId = new StringIdentifier("stat1"), Term = "stat1" };
            }

            private sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
            {
                public IIdentifier StateDefinitionId { get; set; }

                public string Term { get; set; }
            }
        }
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
            yield break;
        }
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

    public sealed class StatPrinterSystem : ISystem
    {
        private readonly IBehaviorFinder _behaviorFinder = new BehaviorFinder();

        public void Update(ISystemUpdateContext systemUpdateContext, IEnumerable<IHasBehaviors> hasBehaviors)
        {
            foreach (var hasBehavior in hasBehaviors)
            {
                Tuple<IHasStats> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                Console.WriteLine($"Stat 1: {behaviours.Item1.Stats[new StringIdentifier("stat1")]}");
            }
        }
    }

    public sealed class ElapsedTimeTriggerMechanicSystem : ISystem
    {
        private readonly IElapsedTimeTriggerSourceMechanic _elapsedTimeTriggerSourceMechanic;

        public ElapsedTimeTriggerMechanicSystem(IElapsedTimeTriggerSourceMechanic elapsedTimeTriggerSourceMechanic)
        {
            _elapsedTimeTriggerSourceMechanic = elapsedTimeTriggerSourceMechanic;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _elapsedTimeTriggerSourceMechanic.Update(elapsed);
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