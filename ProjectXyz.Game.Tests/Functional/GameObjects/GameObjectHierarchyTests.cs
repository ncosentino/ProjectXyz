using System.Linq;

using Autofac;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects
{
    public sealed class GameObjectHierarchyTests
    {
        private static readonly TestFixture _testFixture;
        private static readonly ILifetimeScope _lifetimeScope;
        private static readonly IGameObjectHierarchy _gameObjectHierarchy;
        private static readonly ICalculationPriorityFactory _calculationPriorityFactory;
        private static readonly IActorFactory _actorFactory;
        private static readonly IItemFactory _itemFactory;
        private static readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;

        static GameObjectHierarchyTests()
        {
            var testData = new TestData();
            _testFixture = new TestFixture(testData);
            _lifetimeScope = _testFixture.LifeTimeScope;
            _gameObjectHierarchy = _lifetimeScope.Resolve<IGameObjectHierarchy>();
            _calculationPriorityFactory = _lifetimeScope.Resolve<ICalculationPriorityFactory>();
            _actorFactory = _lifetimeScope.Resolve<IActorFactory>();
            _itemFactory = _lifetimeScope.Resolve<IItemFactory>();
            _hasEnchantmentsBehaviorFactory = _lifetimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
        }

        [Fact]
        private void GetChildrenRecursive_ActorWithEnchantedEquipmentAndItems_AllChildGameObjects()
        {
            var statId = new StringIdentifier("Stat A");
            var statTerm = new StringIdentifier("STAT_A");

            var itemOnlyEnchantment1 = _testFixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 10",
                    _calculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var itemOnlyEnchantment2 = _testFixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} * 2",
                    _calculationPriorityFactory.Create<int>(2),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var actorOnlyEnchantment = _testFixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 3",
                    _calculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner")));
            var inventoryItemOnlyEnchantment = _testFixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 6",
                    _calculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner.owner")));

            var actor = _actorFactory.Create(
                new IdentifierBehavior(),
                Enumerable.Empty<IBehavior>());
            var equipSlotId = actor
                .GetOnly<ICanEquipBehavior>()
                .SupportedEquipSlotIds
                .First();

            var item = _itemFactory.Create(
                new CanBeEquippedBehavior(equipSlotId),
                _hasEnchantmentsBehaviorFactory.Create());
            var equippable = item.GetOnly<ICanBeEquippedBehavior>();
            var itemEnchantmentsBehavior = item.GetOnly<IHasEnchantmentsBehavior>();
            itemEnchantmentsBehavior.AddEnchantments(
                itemOnlyEnchantment1,
                itemOnlyEnchantment2,
                actorOnlyEnchantment);

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId,
                    equippable,
                    false);

            var inventoryItem = _itemFactory
                .Create(_hasEnchantmentsBehaviorFactory.Create());
            var inventoryItemEnchantmentsBehavior = inventoryItem.GetOnly<IHasEnchantmentsBehavior>();
            inventoryItemEnchantmentsBehavior.AddEnchantments(
                itemOnlyEnchantment1,
                inventoryItemOnlyEnchantment);

            var inventory = actor
                .GetOnly<IHasItemContainersBehavior>()
                .ItemContainers
                .Single(c => c.ContainerId.Equals(new StringIdentifier("Inventory")));

            inventory.TryAddItem(inventoryItem);

            var results = _gameObjectHierarchy
                .GetChildren(
                    actor,
                    true)
                .ToArray();

            Assert.Equal(6, results.Length);
            Assert.Contains(item, results);
            Assert.Contains(inventoryItem, results);
            Assert.Contains(itemOnlyEnchantment1, results);
            Assert.Contains(itemOnlyEnchantment2, results);
            Assert.Contains(inventoryItemOnlyEnchantment, results);
            Assert.Contains(actorOnlyEnchantment, results);
        }
    }
}
