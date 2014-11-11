using System;

using Moq;
using Xunit;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Tests.Application.Enchantments.Mocks;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemEnchantingTests
    {
        [Fact]
        public void EnchantItemMaximumDurability()
        {
            var context = new Mock<IItemContext>();
            context
                .Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(context.Object, ProjectXyz.Data.Core.Items.Item.Create());
            var baseDurability = Durability.Create(
                item.Durability.Maximum, 
                item.Durability.Current);
            var enchantment = new Mock<IEnchantment>();
            enchantment
                .Setup(x => x.CalculationId)
                .Returns("Value");
            enchantment
                .Setup(x => x.StatId)
                .Returns(ItemStats.MaximumDurability);
            enchantment
                .Setup(x => x.Value)
                .Returns(100);

            item.Enchant(enchantment.Object);

            Assert.Equal(baseDurability.Current, item.Durability.Current);
            Assert.Equal(baseDurability.Maximum + 100, item.Durability.Maximum);
        }

        [Fact]
        public void EnchantItemToBreak()
        {
            var itemData = new Data.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var context = new MockItemContextBuilder().Build();
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(context, itemData);
            var baseDurability = Durability.Create(
                item.Durability.Maximum,
                item.Durability.Current);
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId("Value")
                .WithStatId(ItemStats.CurrentDurability)
                .WithValue(-100)
                .Build();

            bool gotEvent = false;
            item.Broken += (_, __) => gotEvent = true;
            item.Enchant(enchantment);

            Assert.True(
                gotEvent,
                "Expecting to get broken event.");
            Assert.Equal(0, item.Durability.Current);
            Assert.Equal(baseDurability.Maximum, item.Durability.Maximum);
        }

        [Fact]
        public void BrokenItemsDontBreak()
        {
            var itemData = new Data.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var context = new MockItemContextBuilder().Build();
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(context, itemData);
            var baseDurability = Durability.Create(
                item.Durability.Maximum,
                item.Durability.Current);
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId("Value")
                .WithStatId(ItemStats.CurrentDurability)
                .WithValue(-100)
                .Build();

            item.Enchant(enchantment);
            
            bool gotEvent = false;
            item.Broken += (_, __) => gotEvent = true;

            item.Enchant(enchantment);
            Assert.False(
                gotEvent,
                "Not expecting to get broken event on an already broken item.");
        }
    }
}
