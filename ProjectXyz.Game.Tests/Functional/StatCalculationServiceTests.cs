using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;

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
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var itemOnlyEnchantment2 = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} * 2",
                    _fixture.CalculationPriorityFactory.Create<int>(2),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var actorOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 3",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner")));
            var inventoryItemOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 6",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
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

            var item = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
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
                    equippable);

            var inventoryItem = _fixture
                .ItemFactory
                .Create(_fixture.HasEnchantmentsBehaviorFactory.Create());
            var inventoryItemEnchantmentsBehavior = inventoryItem.GetOnly<IHasEnchantmentsBehavior>();
            inventoryItemEnchantmentsBehavior.AddEnchantments(
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

        [Fact]
        private void GetStatValue_SocketBuff_ExpectedStat()
        {
            var statId = new StringIdentifier("Stat A");
            var statTerm = new StringIdentifier("STAT_A");

            var fitInSocketEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 10",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var canBeSocketedEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 5",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                Enumerable.Empty<IBehavior>());
            var equipSlotId = actor
                .GetOnly<ICanEquipBehavior>()
                .SupportedEquipSlotIds
                .First();

            var socketTypeId = new StringIdentifier("socket type id");

            var canBeSocketedItem = _fixture
                .ItemFactory
                .Create(
                    new ApplySocketEnchantmentsBehavior(),
                    new CanBeSocketedBehavior(new[]
                    {
                        socketTypeId,
                    }),
                    new CanBeEquippedBehavior(equipSlotId),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
            canBeSocketedItem
                .GetOnly<IHasEnchantmentsBehavior>()
                .AddEnchantments(canBeSocketedEnchantment);
            var equippable = canBeSocketedItem.GetOnly<ICanBeEquippedBehavior>();

            var canFitSocketItem = _fixture
                .ItemFactory
                .Create(
                    new CanFitSocketBehavior(socketTypeId, 1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
            canFitSocketItem
                .GetOnly<IHasEnchantmentsBehavior>()
                .AddEnchantments(fitInSocketEnchantment);

            Assert.True(
                canBeSocketedItem
                    .GetOnly<ICanBeSocketedBehavior>()
                    .Socket(canFitSocketItem.GetOnly<ICanFitSocketBehavior>()),
                $"Expected to be able socket '{canFitSocketItem}' into " +
                $"'{canBeSocketedItem}'.");

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId,
                    equippable);

            var canBeSocketedItemStats = canBeSocketedItem.GetOnly<IHasStatsBehavior>();
            var actorStats = actor.GetOnly<IHasStatsBehavior>();
            var canFitSocketItemStats = canFitSocketItem.GetOnly<IHasStatsBehavior>();

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);

            var statCalculationService = _fixture.StatCalculationService;
            Assert.Equal(
                15,
                statCalculationService.GetStatValue(canBeSocketedItem, statId, context));
            Assert.Equal(
                10,
                statCalculationService.GetStatValue(canFitSocketItem, statId, context));
            Assert.Equal(
                15,
                statCalculationService.GetStatValue(actor, statId, context));
        }
    }
}
