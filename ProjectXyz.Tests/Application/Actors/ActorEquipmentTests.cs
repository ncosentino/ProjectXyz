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
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Tests.Application.Enchantments.Mocks;
using ProjectXyz.Tests.Data.Actors.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Application.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorEquipmentTests
    {
        [Fact]
        public void EquipLifeEnchantedItem()
        {
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(ADDITIONAL_LIFE)
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
                    Assert.Equal(BASE_LIFE, actor.CurrentLife);
                    Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.MaximumLife);
                });
        }

        [Fact]
        public void EquipHealItem()
        {
            const double MAX_LIFE = 100;
            const double BASE_LIFE = 50;
            const double HEAL_AMOUNT = 1000;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.CurrentLife)
                .WithValue(HEAL_AMOUNT)
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
                    Assert.Equal(MAX_LIFE, actor.CurrentLife);
                    Assert.Equal(MAX_LIFE, actor.MaximumLife);
                });
        }

        [Fact]
        public void EquipBrokenItem()
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
        public void BreakingEquipmentRemovesEnchantment()
        {
            var durabilityEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ItemStats.CurrentDurability)
                .WithValue(-100)
                .Build();

            var lifeEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(100)
                .Build();

            var enchantedItem = ProjectXyz.Application.Core.Items.ItemBuilder.Create()
                .WithMaterialFactory(new Mock<ProjectXyz.Data.Interface.Items.Materials.IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    new Data.Items.Mocks.MockItemBuilder()
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
                () => Assert.Equal(lifeEnchantment.Value, actor.MaximumLife));
            enchantedItem.Enchant(durabilityEnchantment);

            Assert.Equal(0, actor.MaximumLife);
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
                    "Expecting item to be equipped in one of [" + string.Join(", ", item.EquippableSlots) + "] slots.");
            }

            if (validationCallback != null)
            {
                validationCallback.Invoke();
            }
        }
    }
}
