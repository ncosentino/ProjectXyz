using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Systems;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;
using ProjectXyz.Plugins.Triggers.Enchantments;
using ProjectXyz.Plugins.Triggers.Enchantments.Expiration;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

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

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();

            var actorFactory = dependencyContainer.Resolve<IActorFactory>();
            var actor = actorFactory.Create();

            var buffable = actor
                .Behaviors
                .GetFirst<IBuffable>();
            buffable.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new StringIdentifier("stat1"),
                    new IComponent[]
                    {
                        new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat1 + 1"),
                        new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
                        new AppliesToBaseStat(),
                    }),
            });

            var itemFactory = dependencyContainer.Resolve<IItemFactory>();
            var item = itemFactory.Create();

            var buffableItem = actor
                .Behaviors
                .GetFirst<IBuffable>();
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
                .GetFirst<ICanEquip>();
            canEquip.TryEquip(
                new StringIdentifier("left hand"),
                item.Behaviors.GetFirst<ICanBeEquipped>());

            dependencyContainer
                .Resolve<IMutableGameObjectManager>()
                .MarkForAddition(actor);

            var cancellationTokenSource = new CancellationTokenSource();
            gameEngine.Start(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }

    public interface IActorFactory
    {
        Actor Create();
    }

    public sealed class ActorFactory : IActorFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;

        public ActorFactory(
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IBehaviorManager behaviorManager)
        {
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorManager = behaviorManager;
        }

        public Actor Create()
        {
            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStats(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantments(activeEnchantmentManager);
            var buffable = new Buffable(activeEnchantmentManager);
            var canEquip = new CanEquipBehavior();
            var applyEquipmentEnchantmentsBehavior = new ApplyEquipmentEnchantmentsBehavior();
            var actor = new Actor(
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canEquip,
                applyEquipmentEnchantmentsBehavior);
            return actor;
        }
    }

    public interface IItemFactory
    {
        Item Create();
    }

    public sealed class ItemFactory : IItemFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;

        public ItemFactory(
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IBehaviorManager behaviorManager)
        {
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorManager = behaviorManager;
        }

        public Item Create()
        {
            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStats(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantments(activeEnchantmentManager);
            var buffable = new Buffable(activeEnchantmentManager);
            var canBeEquipped = new CanBeEquippedBehavior();
            var item = new Item(
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canBeEquipped);
            return item;
        }
    }

    public interface ICanBeEquipped : IBehavior
    {
        
    }

    public sealed class CanBeEquippedBehavior : 
        BaseBehavior,
        ICanBeEquipped
    {
        
    }

    public interface IHasEquipment : IBehavior
    {
        bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquipped canBeEquipped);
    }

    public interface ICanEquip : IHasEquipment
    {
        event EventHandler<EventArgs<Tuple<ICanEquip, ICanBeEquipped>>> Equipped;

        bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquipped canBeEquipped);

        bool CanEquip(
            IIdentifier equipSlotId,
            ICanBeEquipped canBeEquipped);

        bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquipped canBeEquipped);
    }

    public sealed class CanEquipBehavior :
        BaseBehavior,
        ICanEquip
    {
        private readonly Dictionary<IIdentifier, ICanBeEquipped> _equipment;

        public CanEquipBehavior()
        {
            _equipment = new Dictionary<IIdentifier, ICanBeEquipped>();
        }

        public event EventHandler<EventArgs<Tuple<ICanEquip, ICanBeEquipped>>> Equipped;

        public bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquipped canBeEquipped)
        {
            return _equipment.TryGetValue(
                equipSlotId,
                out canBeEquipped) &&
                _equipment.Remove(equipSlotId);
        }

        public bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquipped canBeEquipped)
        {
            return _equipment.TryGetValue(
                equipSlotId,
                out canBeEquipped);
        }

        public bool CanEquip(
            IIdentifier equipSlotId,
            ICanBeEquipped canBeEquipped)
        {
            // TODO: check all the requirements...
            return _equipment.ContainsKey(equipSlotId);
        }

        public bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquipped canBeEquipped)
        {
            if (CanEquip(
                equipSlotId,
                canBeEquipped))
            {
                return false;
            }

            _equipment[equipSlotId] = canBeEquipped;

            Equipped?.Invoke(
                this,
                new EventArgs<Tuple<ICanEquip, ICanBeEquipped>>(new Tuple<ICanEquip, ICanBeEquipped>(
                    this,
                    canBeEquipped)));
            return true;
        }
    }

    public interface IApplyEquipmentEnchantmentsBehavior : IBehavior
    {
    }

    public sealed class ApplyEquipmentEnchantmentsBehavior :
        BaseBehavior,
        IApplyEquipmentEnchantmentsBehavior
    {
        private ICanEquip _canEquip;

        protected override void OnRegisteredToOwner(IHasBehaviors owner)
        {
            base.OnRegisteredToOwner(owner);

            if (owner.Behaviors.TryGetFirst(out _canEquip))
            {
                _canEquip.Equipped += CanEquip_Equipped;
            }
        }

        private void CanEquip_Equipped(
            object sender, 
            EventArgs<Tuple<ICanEquip, ICanBeEquipped>> e)
        {
            IBuffable buffable;
            IHasEnchantments hasEnchantments;
            if (e.Data.Item1.Owner.Behaviors.TryGetFirst(out buffable) &&
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out hasEnchantments))
            {
                buffable.AddEnchantments(hasEnchantments.Enchantments);
            }
        }
    }

    public sealed class Item : IGameObject
    {
        public Item(
            IBehaviorManager behaviorManager,
            IHasEnchantments hasEnchantments,
            IBuffable buffable,
            IHasMutableStats hasStats,
            ICanBeEquipped canBeEquipped)
        {
            Behaviors = new BehaviorCollection(
                hasEnchantments,
                buffable,
                hasStats,
                canBeEquipped);
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }

    public sealed class Actor : IGameObject
    {
        public Actor(
            IBehaviorManager behaviorManager,
            IHasEnchantments hasEnchantments,
            IBuffable buffable,
            IHasMutableStats hasStats,
            ICanEquip canEquip,
            IApplyEquipmentEnchantmentsBehavior applyEquipmentEnchantmentsBehavior)
        {
            Behaviors = new BehaviorCollection(
                hasEnchantments,
                buffable,
                hasStats,
                canEquip,
                applyEquipmentEnchantmentsBehavior);
            behaviorManager.Register(this, Behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
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
                Tuple<IHasStats, IHasEnchantments> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                var statCalculationContext =
                    new StatCalculationContext(
                        new GenericComponent<IStateContextProvider>(_stateContextProvider).Yield(),
                        behaviours.Item2.Enchantments)
                    .WithoutBaseStatEnchantments();

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