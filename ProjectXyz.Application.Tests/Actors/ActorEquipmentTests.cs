using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Moq;
using Xunit;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Actors.ExtensionMethods;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Actors.ExtensionMethods;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Data.Tests.Actors.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Application.Tests.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorEquipmentTests
    {
        [Fact]
        public void Actor_EquipEnchantedItem_BoostsStat()
        {
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(ADDITIONAL_LIFE)
                .WithTrigger(EnchantmentTriggers.Equip)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantments(enchantment)
                .WithEquippableSlots("Some slot")
                .WithDurability(1, 1)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder()
                .WithStats(Stat.Create(ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

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
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(ADDITIONAL_LIFE)
                .WithTrigger(EnchantmentTriggers.Hold)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantments(enchantment)
                .WithEquippableSlots("Some slot")
                .WithDurability(1, 1)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder()
                .WithStats(Stat.Create(ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

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
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(ADDITIONAL_LIFE)
                .WithTrigger(EnchantmentTriggers.Hold)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantments(enchantment)
                .WithEquippableSlots("Some slot")
                .WithDurability(1, 1)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder()
                .WithStats(Stat.Create(ActorStats.MaximumLife, BASE_LIFE))
                .WithStats(Stat.Create(ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            Assert.True(
                actor.TakeItem(enchantedItem),
                "Expecting the actor to take the item");
            Assert.Equal(BASE_LIFE, actor.GetCurrentLife());
            Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.GetMaximumLife());
        }

        [Fact]
        public void Actor_EquipHealItem_CappedAtMaxLife()
        {
            const double MAX_LIFE = 100;
            const double BASE_LIFE = 50;
            const double HEAL_AMOUNT = 1000;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.CurrentLife)
                .WithValue(HEAL_AMOUNT)
                .WithTrigger(EnchantmentTriggers.Equip)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantments(enchantment)
                .WithEquippableSlots("Some slot")
                .WithDurability(1, 1)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder()
                .WithStats(Stat.Create(ActorStats.MaximumLife, MAX_LIFE))
                .WithStats(Stat.Create(ActorStats.CurrentLife, BASE_LIFE))
                .Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

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
        public void Actor_EquipBrokenItem_Fails()
        {
            var item = new MockItemBuilder()
                .WithEquippableSlots("Some slot")
                .WithDurability(100, 0)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder().Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            AssertCannotEquipItem(actor, item);
        }

        [Fact]
        public void Actor_BreakingEquipment_RemovesEnchantment()
        {
            var durabilityEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ItemStats.CurrentDurability)
                .WithValue(-100)
                .WithTrigger(EnchantmentTriggers.Item)
                .Build();

            var lifeEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(100)
                .WithTrigger(EnchantmentTriggers.Equip)
                .Build();

            var enchantedItem = ProjectXyz.Application.Core.Items.ItemBuilder.Create()
                .WithMaterialFactory(new Mock<ProjectXyz.Data.Interface.Items.Materials.IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    new Data.Tests.Items.Mocks.MockItemBuilder()
                    .WithStats(
                        Stat.Create(ItemStats.CurrentDurability, 50), 
                        Stat.Create(ItemStats.MaximumDurability, 50))
                    .WithEquippableSlots("Some Slot")
                    .Build());
            enchantedItem.Enchant(lifeEnchantment);

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder().Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            AssertEquipItem(
                actor, 
                enchantedItem,
                () => Assert.Equal(lifeEnchantment.Value, actor.GetMaximumLife()));
            enchantedItem.Enchant(durabilityEnchantment);

            Assert.Equal(0, actor.GetMaximumLife());
        }

        [Fact]
        public void Actor_BreakingEquipment_UnequipsItem()
        {
            var durabilityEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ItemStats.CurrentDurability)
                .WithValue(-100)
                .WithTrigger(EnchantmentTriggers.Item)
                .Build();

            var item = ProjectXyz.Application.Core.Items.ItemBuilder.Create()
                .WithMaterialFactory(new Mock<ProjectXyz.Data.Interface.Items.Materials.IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    new Data.Tests.Items.Mocks.MockItemBuilder()
                    .WithStats(
                        Stat.Create(ItemStats.CurrentDurability, 50),
                        Stat.Create(ItemStats.MaximumDurability, 50))
                    .WithEquippableSlots("Some Slot")
                    .Build());

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder().Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            AssertEquipItem(actor, item);
            item.Enchant(durabilityEnchantment);

            Assert.False(
                actor.Equipment.HasItemEquipped(item),
                "Expecting the item to be unequipped.");
            Assert.Equal(item, new List<IItem>(actor.Inventory.Items)[0]);
        }

        [Fact]
        public void Actor_BlessEnchantment_RemovesCurse()
        {
            var curseEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(-10)
                .WithTrigger(EnchantmentTriggers.Equip)
                .WithStatusType(EnchantmentStatuses.Curse)
                .Build();

            var cursedItem = new MockItemBuilder()
                .WithEnchantments(curseEnchantment)
                .WithEquippableSlots("Some slot")
                .WithDurability(1, 1)
                .Build();

            var blessEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.Bless)
                .WithValue(0)
                .WithTrigger(EnchantmentTriggers.Equip)
                .Build();

            var blessItem = new MockItemBuilder()
                .WithEnchantments(blessEnchantment)
                .WithEquippableSlots("Magical Potion Drinking Slot")
                .WithDurability(1, 1)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder()
                .WithStats(Stat.Create(ActorStats.MaximumLife, 100))
                .WithStats(Stat.Create(ActorStats.CurrentLife, 100))
                .Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            AssertEquipItem(
                actor,
                cursedItem,
                () => Assert.Equal(90, actor.GetMaximumLife()));

            AssertEquipItem(
                actor,
                blessItem,
                () => Assert.Equal(100, actor.GetMaximumLife()));
        }

        public void AssertCannotEquipItem(IActor actor, IItem item)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(item != null);

            Assert.False(
                actor.HasItemEquipped(item),
                "The actor already has '" + item + "' equipped.");
            Assert.False(
                actor.Equip(item),
                "Expecting to fail to equip '" + item + "'.");
            Assert.False(
                actor.HasItemEquipped(item),
                "The actor should not have '" + item + "' equipped.");
        }

        public void AssertEquipItem(IActor actor, IItem item, Action validationCallback = null)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(item != null);

            AssertEquipItems(
                actor,
                new IItem[] { item },
                validationCallback);
        }

        public void AssertEquipItems(IActor actor, IEnumerable<IItem> items, Action validationCallback = null)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(items != null);

            foreach (var item in items)
            {
                Assert.NotNull(item);
                Assert.True(
                    actor.Equip(item),
                    "Expecting to equip '" + item  + "'.");
                Assert.True(
                    actor.HasItemEquipped(item),
                    "Expecting item to be equipped in one of [" + string.Join(", ", item.EquippableSlots.ToArray()) + "] slots.");
            }

            if (validationCallback != null)
            {
                validationCallback.Invoke();
            }
        }
    }
}
