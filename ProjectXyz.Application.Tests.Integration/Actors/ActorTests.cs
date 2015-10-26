using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Actors.ExtensionMethods;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Actors.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Application.Tests.Integration.Helpers;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Tests.Unit.Actors.Mocks;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Integration.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorEquipmentTests
    {
        #region Constants
        private static readonly Guid HEAD_EQUIP_SLOT = Guid.NewGuid(); // TODO: remove this when we can populate possible equip slots on equipment classes
        private static readonly Guid MAGIC_POTION_EQUIP_SLOT = Guid.NewGuid();
        #endregion

        #region Methods
        [Fact]
        public void Actor_EquipEnchantedItem_BoostsStat()
        {
            // Setup
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", ADDITIONAL_LIFE) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var enchantedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               new[] { enchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            // Execute + Assert
            AssertEquipItem(
                actor,
                enchantedItem,
                () =>
                {
                    Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
                    Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.GetMaximumLife());
                });
        }

        [Fact]
        public void Actor_EquipHoldTrigger_TriggersNothing()
        {
            // Setup
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Hold,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", ADDITIONAL_LIFE) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var enchantedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               new[] { enchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            // Execute + Assert
            AssertEquipItem(
                actor,
                enchantedItem,
                () =>
                {
                    Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
                    Assert.Equal(BASE_LIFE, actor.GetMaximumLife());
                });
        }

        [Fact]
        public void Actor_HoldHoldTrigger_BoostsStat()
        {
            // Setup
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Hold,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", ADDITIONAL_LIFE) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var enchantedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               new[] { enchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            // Execute
            actor.Inventory.Add(enchantedItem, 0);
            
            // Assert
            Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
            Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.GetMaximumLife());
        }

        [Fact]
        public void Actor_HeldItemEnchantmentsChanged_StatsAreRecalculated()
        {
            // Setup
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Hold,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", ADDITIONAL_LIFE) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var itemToBeEnchanted = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               Enumerable.Empty<IEnchantment>(),
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            actor.Inventory.Add(itemToBeEnchanted, 0);

            // Execute
            itemToBeEnchanted.Enchant(enchantment);

            // Assert
            Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
            Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.GetMaximumLife());
        }

        [Fact]
        public void Actor_EquipHealItem_CappedAtMaxLife()
        {
            // Setup
            const double MAX_LIFE = 100;
            const double BASE_LIFE = 50;
            const double HEAL_AMOUNT = 1000;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.CurrentLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.CurrentLife) },
                new[] { new KeyValuePair<string, double>("VALUE", HEAL_AMOUNT) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var enchantedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               new[] { enchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, MAX_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            // Execute + Assert
            AssertEquipItem(
                actor,
                enchantedItem,
                () =>
                {
                    Assert.Equal(MAX_LIFE, actor.GetCurrentLife());
                    Assert.Equal(MAX_LIFE, actor.GetMaximumLife());
                });
        }

        [Fact]
        public void Actor_EquippedItemChangesEnchantments_StatsAreRecalculated()
        {
            // Setup
            const double LIFE_BUFF = 100;
            const double BASE_LIFE = 50;

            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", LIFE_BUFF) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var itemToBeEnchanted = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 1),
                },
               Enumerable.Empty<IEnchantment>(),
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);
            
            AssertEquipItem(
                actor,
                itemToBeEnchanted,
                () =>
                {
                    // Execute
                    itemToBeEnchanted.Enchant(enchantment);

                    // Assert
                    Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
                    Assert.Equal(BASE_LIFE + LIFE_BUFF, actor.GetMaximumLife());
                });
        }

        [Fact]
        public void Actor_EquipBrokenItem_Fails()
        {
            // Setup
            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var item = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, 0),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, 10),
                },
               Enumerable.Empty<IEnchantment>(),
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder().Build();

            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            AssertCannotEquipItem(actor, item, item.EquippableSlotIds.First());
        }

        [Fact]
        public void Actor_BreakingEquipment_RemovesEnchantment()
        {
            // Setup
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;
            const double MAX_DURABILITY = 100;

            var lifeEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", ADDITIONAL_LIFE) });

            var breakingEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Item,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ItemStats.CurrentDurability,
                "DUR + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("DUR", ItemStats.CurrentDurability) },
                new[] { new KeyValuePair<string, double>("VALUE", -MAX_DURABILITY) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var enchantedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, MAX_DURABILITY),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, MAX_DURABILITY),
                },
               new[] { lifeEnchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            AssertEquipItem(
                actor,
                enchantedItem,
                () => Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.GetMaximumLife()));

            // Execute
            enchantedItem.Enchant(breakingEnchantment);

            // Assert
            Assert.Equal(BASE_LIFE, actor.GetMaximumLife());
        }

        [Fact]
        public void Actor_BreakingEquipment_UnequipsItem()
        {
            // Setup
            const double MAX_DURABILITY = 100;
            
            var breakingEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Item,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ItemStats.CurrentDurability,
                "DUR + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("DUR", ItemStats.CurrentDurability) },
                new[] { new KeyValuePair<string, double>("VALUE", -MAX_DURABILITY) });

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var item = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, MAX_DURABILITY),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, MAX_DURABILITY),
                },
               Enumerable.Empty<IEnchantment>(),
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);

            AssertEquipItem(
                actor,
                item);

            // Execute
            item.Enchant(breakingEnchantment);

            // Assert
            Assert.False(
                actor.Equipment.HasItemEquipped(item),
                "Expecting the item to be unequipped.");
            Assert.Equal(item, new List<IItem>(actor.Inventory)[0]);
        }

        [Fact]
        public void Actor_EnchantmentWithNegation_StatusIsNegated()
        {
            // Setup
            const double CURSE_LIFE_AMOUNT = -10;
            const double BASE_LIFE = 100;

            var curseEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                EnchantmentStatuses.Curse,
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "LIFE + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("LIFE", ActorStats.MaximumLife) },
                new[] { new KeyValuePair<string, double>("VALUE", CURSE_LIFE_AMOUNT) });

            var blessEnchantment = OneShotNegateEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Equip,
                Guid.NewGuid(),
                ActorStats.Bless);

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = ItemContextHelper.CreateItemContext();

            var cursedItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
               {
                   Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, -1),
               },
               new[] { curseEnchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { HEAD_EQUIP_SLOT });

            var blessItem = Item.Create(
               itemContext,
               Guid.NewGuid(),
               Guid.NewGuid(),
               itemMetaData.Object,
               Enumerable.Empty<IItemNamePart>(),
               itemRequirements.Object,
               new[]
               {
                   Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, -1),
               },
               new[] { blessEnchantment },
               Enumerable.Empty<IItemAffix>(),
               Enumerable.Empty<IItem>(),
               new[] { MAGIC_POTION_EQUIP_SLOT });

            var data = new MockActorBuilder()
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(Guid.NewGuid(), ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = CreateMockContext();

            var actor = Actor.Create(
                context.Object,
                data);
            
            AssertEquipItem(
                actor,
                cursedItem,
                () => Assert.Equal(BASE_LIFE + CURSE_LIFE_AMOUNT, actor.GetMaximumLife()));

            // Execute + Assert
            AssertEquipItem(
                actor,
                blessItem,
                () => Assert.Equal(BASE_LIFE, actor.GetMaximumLife()));
        }

        public void AssertCannotEquipItem(IActor actor, IItem item, Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(item != null);

            var preHasItemEquipped = actor.HasItemEquipped(item);
            if (preHasItemEquipped)
            {
                // split this into a separate check to avoid calling ToString() unnecessarily
                Assert.False(
                    true,
                    "The actor already has '" + item + "' equipped.");                
            }

            var canEquipitem = actor.CanEquip(item, slotId);
            if (canEquipitem)
            {
                // split this into a separate check to avoid calling ToString() unnecessarily
                Assert.False(
                    true,
                    "Expecting to fail to equip '" + item + "' to '" + slotId + "'.");
            }
            
            // TODO: actually incorporate this when bad equipping can throw
            //Assert.Throws<InvalidOperationException>(() => actor.Equip(item, slotId));
            
            var postHasItemEquipped = actor.HasItemEquipped(item);
            if (postHasItemEquipped)
            {
                // split this into a separate check to avoid calling ToString() unnecessarily
                Assert.False(
                    true,
                    "The should not have '" + item + "' equipped.");
            }
        }

        private void AssertEquipItem(IActor actor, IItem item, Action validationCallback = null)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(item != null);

            AssertEquipItems(
                actor,
                new IItem[] { item },
                validationCallback);
        }

        private void AssertEquipItems(IActor actor, IEnumerable<IItem> items, Action validationCallback = null)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(items != null);

            foreach (var item in items)
            {
                var slotId = item.EquippableSlotIds.First();
                Assert.NotNull(item);

                var canEquip = actor.CanEquip(item, slotId);
                if (!canEquip)
                {
                    // split this into a separate check to avoid calling ToString() unnecessarily
                    Assert.True(false, "Expecting to equip '" + item  + "' to '" + slotId + "'.");
                }

                actor.Equip(item, slotId);
                Assert.True(
                    actor.HasItemEquipped(item),
                    "Expecting item to be equipped in one of [" + string.Join(", ", item.EquippableSlotIds.Select(x => x.ToString()).ToArray()) + "] slots.");
            }

            if (validationCallback != null)
            {
                validationCallback.Invoke();
            }
        }

        private Mock<IActorContext> CreateMockContext()
        {
            var enchantmentCalculator = EnchantmentCalculatorHelper.CreateEnchantmentCalculator();

            var context = new Mock<IActorContext>();
            context
                .Setup(x => x.EnchantmentCalculator)
                .Returns(enchantmentCalculator);
            return context;
        }
        #endregion
    }
}