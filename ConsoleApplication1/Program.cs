using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Autofac;
using Autofac.Core;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Core.Enchantments;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Systems;
using ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;
using Module = Autofac.Module;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules =
                moduleDiscoverer.Discover("*.exe")
                .Concat(moduleDiscoverer
                .Discover("*.Dependencies.Autofac.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            ////var gameEngine = new GameEngine(
            ////    gameObjectManager,
            ////    new ISystem[]
            ////    {
            ////        new GameObjectManagerSystem(gameObjectManager), 
            ////        statUpdaterSystem,
            ////        new StatPrinterSystem(),
            ////        new ElapsedTimeTriggerMechanicSystem(elapsedTimeTriggerSourceMechanic), 
            ////    },
            ////    elapsedTimeComponentCreator.Yield());

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();

            dependencyContainer
                .Resolve<IMutableGameObjectManager>()
                .MarkForAddition(CreateActor(
                    dependencyContainer.Resolve<IStatDefinitionToTermMappingRepository>(),
                    dependencyContainer.Resolve<IActiveEnchantmentManager>()));

            var cancellationTokenSource = new CancellationTokenSource();
            gameEngine.Start(cancellationTokenSource.Token);

            Console.ReadLine();
        }

        private static Actor CreateActor(
            IStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IActiveEnchantmentManager activeEnchantmentManager)
        {
            var hasEnchantments = new HasEnchantments(activeEnchantmentManager);
            var buffable = new Buffable(activeEnchantmentManager);
            var mutableStatsProvider = new MutableStatsProvider(
                statDefinitionToTermMappingRepository
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(x => x.StateDefinitionId, _ => 0d));
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

    ////public sealed class StatsPlugin : IStatPlugin
    ////{
    ////    public StatsPlugin()
    ////    {
    ////        SharedComponents = new ComponentCollection(new[]
    ////        {
    ////            new GenericComponent<IStatDefinitionToTermMappingRepository>(new StatDefinitionToTermMappingRepo()),
    ////        });
    ////    }

    ////    public IComponentCollection SharedComponents { get; }

    ////    private sealed class StatDefinitionToTermMappingRepo : IStatDefinitionToTermMappingRepository
    ////    {
    ////        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
    ////        {
    ////            yield return new StatDefinitionToTermMapping() { StateDefinitionId = new StringIdentifier("stat1"), Term = "stat1" };
    ////        }

    ////        private sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
    ////        {
    ////            public IIdentifier StateDefinitionId { get; set; }

    ////            public string Term { get; set; }
    ////        }
    ////    }
    ////}



    




    



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
        private readonly IInterval _updateInterval = new Interval<double>(1000);
        private IInterval _elapsed;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .Components
                .Get<IComponent<IElapsedTime>>()
                .First()
                .Value
                .Interval;
            _elapsed = _elapsed == null
                ? elapsed
                : _elapsed.Add(elapsed);
            if (_elapsed.CompareTo(_updateInterval) < 0)
            {
                return;
            }

            _elapsed = null;

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