using System;

using Moq;
using Xunit;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemEnchantingTests
    {
        [Fact]
        public void Item_EnchantMaximumDurability_BoostsStat()
        {
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    ProjectXyz.Data.Core.Items.Item.Create());
            var baseDurability = Durability.Create(
                item.Durability.Maximum, 
                item.Durability.Current);
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ItemStats.MaximumDurability)
                .WithValue(100)
                .Build();

            item.Enchant(enchantment);

            Assert.Equal(baseDurability.Current, item.Durability.Current);
            Assert.Equal(baseDurability.Maximum + 100, item.Durability.Maximum);
        }

        [Fact]
        public void Item_EnchantNegativeCurrentDurability_BreaksItem()
        {
            var itemData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(), 
                    itemData);
            var baseDurability = Durability.Create(
                item.Durability.Maximum,
                item.Durability.Current);
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
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
        public void Item_EnchantNegativeDurabilityBrokenItem_DoesNotBreak()
        {
            var itemData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(), 
                    itemData);
            var baseDurability = Durability.Create(
                item.Durability.Maximum,
                item.Durability.Current);
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
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
