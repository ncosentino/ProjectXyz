using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Systems;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Extensions.Collections;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Api;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;
using ProjectXyz.Plugins.Triggers.Enchantments.Expiration;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Framework.Entities.Extensions;
using ProjectXyz.Game.Core.Items;
using ProjectXyz.Game.Interface.GameObjects.Actors;
using ProjectXyz.Game.Interface.GameObjects.Items;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var moduleDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var modules =
                moduleDiscoverer.Discover(moduleDirectory, "*.exe")
                .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "*.Dependencies.Autofac.dll"))
                .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "Examples.Modules.*.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            var itemGenerationContextFactory = dependencyContainer.Resolve<IItemGenerationContextFactory>();
            var itemGenerationContext = itemGenerationContextFactory.Merge(
                itemGenerationContextFactory.Create(),
                new []
                {
                    new ItemCountContextComponent(1, 1), 
                });
            var generatedItems = dependencyContainer
                .Resolve<IItemGenerator>()
                .GenerateItems(itemGenerationContext)
                .ToArray();

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();

            var actorFactory = dependencyContainer.Resolve<IActorFactory>();
            var actor = actorFactory.Create();

            var buffable = actor
                .Behaviors
                .GetFirst<IBuffableBehavior>();
            buffable.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new StringIdentifier("stat1"),
                    new IComponent[]
                    {
                        new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat1 + 1"),
                        new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
                        dependencyContainer.Resolve<IAppliesToBaseStat>(),
                    }),
            });

            var item = generatedItems.First();

            var buffableItem = item
                .Behaviors
                .GetFirst<IBuffableBehavior>();
            buffableItem.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new StringIdentifier("stat2"),
                    new IComponent[]
                    {
                        new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat2 + 1"),
                        new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
                    }),
            });

            var canEquip = actor
                .Behaviors
                .GetFirst<ICanEquipBehavior>();
            canEquip.TryEquip(
                new StringIdentifier("left hand"),
                item.Behaviors.GetFirst<ICanBeEquippedBehavior>());

            dependencyContainer
                .Resolve<IMutableGameObjectManager>()
                .MarkForAddition(actor);

            var cancellationTokenSource = new CancellationTokenSource();
            gameEngine.Start(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }

    

    

    public sealed class StatPrinterSystem : ISystem
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IBehaviorFinder _behaviorFinder = new BehaviorFinder();
        private readonly IInterval _updateInterval = new Interval<double>(1000);
        private IInterval _elapsed;

        public StatPrinterSystem(IStateContextProvider stateContextProvider)
        {
            _stateContextProvider = stateContextProvider;
        }

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
                Tuple<IHasStatsBehavior, IHasEnchantmentsBehavior> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                var statCalculationContext =
                    new StatCalculationContext(
                        new GenericComponent<IStateContextProvider>(_stateContextProvider).Yield(),
                        behaviours
                            .Item2
                            .Enchantments
                            .Where(x => !x.Components.Has<IAppliesToBaseStat>()));

                Console.WriteLine($"Base Stat 1: {behaviours.Item1.BaseStats.GetValueOrDefault(new StringIdentifier("stat1"))}");
                Console.WriteLine($"Calc'd Stat 1: {behaviours.Item1.GetStatValue(statCalculationContext, new StringIdentifier("stat1"))}");
                Console.WriteLine($"Base Stat 2: {behaviours.Item1.BaseStats.GetValueOrDefault(new StringIdentifier("stat2"))}");
                Console.WriteLine($"Calc'd Stat 2: {behaviours.Item1.GetStatValue(statCalculationContext, new StringIdentifier("stat2"))}");
                Console.WriteLine($"# Enchantments: {behaviours.Item2.Enchantments.Count}");
                Console.WriteLine("----");
            }
        }
    }
}