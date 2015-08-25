using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemEnchantingTests
    {
        [Fact]
        public void Item_EnchantMaximumDurability_BoostsStat()
        {
            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                ItemStore.Create(
                    Guid.NewGuid(),
                    "Item",
                    "Resource",
                    "Type",
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    new[] { "" }));
            var baseCurrentDurability = item.CurrentDurability;
            var baseMaximumDurability = item.MaximumDurability;
            var enchantment = new MockExpressionEnchantmentBuilder()
                .WithStatId(ItemStats.MaximumDurability)
                .WithExpression("Value")
                .WithValue("Value", 100)
                .Build();

            item.Enchant(enchantment);

            Assert.Equal(baseCurrentDurability, item.CurrentDurability);
            Assert.Equal(baseMaximumDurability + 100, item.MaximumDurability);
        }

        [Fact]
        public void Item_EnchantNegativeCurrentDurability_BreaksItem()
        {
            var itemData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var item = Item.Create(
                new MockItemContextBuilder().Build(), 
                itemData);
            var baseMaximumDurability = item.MaximumDurability;
            var enchantment = new MockExpressionEnchantmentBuilder()
                .WithStatId(ItemStats.CurrentDurability)
                .WithExpression("Value")
                .WithValue("Value", -100)
                .Build();

            bool gotEvent = false;
            item.DurabilityChanged += (sender, __) =>
            {
                if (((IItem)sender).CurrentDurability <= 0)
                {
                    gotEvent = true;
                }
            };
            item.Enchant(enchantment);

            Assert.True(
                gotEvent,
                "Expecting to get broken event.");
            Assert.Equal(0, item.CurrentDurability);
            Assert.Equal(baseMaximumDurability, item.MaximumDurability);
        }

        [Fact]
        public void Item_EnchantNegativeDurabilityBrokenItem_DoesNotBreak()
        {
            var itemData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.CurrentDurability, 50),
                    Stat.Create(ItemStats.MaximumDurability, 50))
                .Build();

            var item = Item.Create(
                new MockItemContextBuilder().Build(), 
                itemData);
            var enchantment = new MockExpressionEnchantmentBuilder()
                .WithStatId(ItemStats.CurrentDurability)
                .WithExpression("Value")
                .WithValue("Value", -100)
                .Build();

            item.Enchant(enchantment);
            
            bool gotEvent = false;
            item.DurabilityChanged += (sender, __) =>
            {
                if (((IItem)sender).CurrentDurability <= 0)
                {
                    gotEvent = true;
                }
            };

            item.Enchant(enchantment);
            Assert.False(
                gotEvent,
                "Not expecting to get broken event on an already broken item.");
        }
    }
}
