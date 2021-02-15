using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class StatCalculationServiceTests
    {
        private static readonly TestFixture _fixture;

        static StatCalculationServiceTests()
        {
            _fixture = new TestFixture(new TestData());
        }

        [Fact]
        private void GetStatValue_InventoryBuffEquipmentBuff_ExpectedStat()
        {
            var statId = new StringIdentifier("Stat A");
            var statTerm = new StringIdentifier("STAT_A");

            var itemOnlyEnchantment1 = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 10",
                    new CalculationPriority<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var itemOnlyEnchantment2 = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} * 2",
                    new CalculationPriority<int>(2),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var actorOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 3",
                    new CalculationPriority<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner")));
            var inventoryItemOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 6",
                    new CalculationPriority<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner.owner")));

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                Enumerable.Empty<IBehavior>());
            var equipSlotId = actor
                .GetOnly<ICanEquipBehavior>()
                .SupportedEquipSlotIds
                .First();

            var itemEnchantmentManager = _fixture.ActiveEnchantmentManagerFactory.Create();
            var item = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId),
                    new BuffableBehavior(itemEnchantmentManager),
                    new HasEnchantmentsBehavior(itemEnchantmentManager));
            var equippable = item.GetOnly<ICanBeEquippedBehavior>();
            var buffableItem = item.GetOnly<IBuffableBehavior>();
            buffableItem.AddEnchantments(
                itemOnlyEnchantment1,
                itemOnlyEnchantment2,
                actorOnlyEnchantment);

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId,
                    equippable);

            var inventoryItemEnchantmentManager = _fixture.ActiveEnchantmentManagerFactory.Create();
            var inventoryItem = _fixture
                .ItemFactory
                .Create(
                    new BuffableBehavior(inventoryItemEnchantmentManager),
                    new HasEnchantmentsBehavior(inventoryItemEnchantmentManager));
            var buffableInventoryItem = inventoryItem.GetOnly<IBuffableBehavior>();
            buffableInventoryItem.AddEnchantments(
                itemOnlyEnchantment1,
                inventoryItemOnlyEnchantment);

            var inventory = actor
                .GetOnly<IHasItemContainersBehavior>()
                .ItemContainers
                .Single(c => c.ContainerId.Equals(new StringIdentifier("Inventory")));

            inventory.TryAddItem(inventoryItem);

            var itemStats = item.GetOnly<IHasStatsBehavior>();
            var actorStats = actor.GetOnly<IHasStatsBehavior>();
            var inventoryItemStats = inventoryItem.GetOnly<IHasStatsBehavior>();

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);

            var statCalculationService = _fixture.StatCalculationService;
            Assert.Equal(
                20,
                statCalculationService.GetStatValue(item, statId, context));
            Assert.Equal(
                10,
                statCalculationService.GetStatValue(inventoryItem, statId, context));
            Assert.Equal(
                29,
                statCalculationService.GetStatValue(actor, statId, context));
        }
    }
}
