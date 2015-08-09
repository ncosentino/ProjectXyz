using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Application.Tests.Xunit.Assertions.Enchantments;
using ProjectXyz.Application.Tests.Xunit.Assertions.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemIntegrationTests
    {
        [Fact]
        public void Item_SaveAndLoadNoEnchantments_CreatesEquivalentItem()
        {
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.Value, 1234567))
                .WithEquippableSlots("Slot 1", "Slot 2")
                .Build();
            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                sourceData);

            var enchantmentSaver = EnchantmentSaver.Create();
            var itemSaver = ItemSaver.Create(enchantmentSaver);
            var savedData = itemSaver.Save(item);

            var rebuiltItem = Item.Create(
                new MockItemContextBuilder().Build(),
                savedData);

            // metadata
            Assert.Equal(item.Id, rebuiltItem.Id);
            Assert.Equal(item.ItemType, rebuiltItem.ItemType);
            Assert.Equal(item.MagicTypeId, rebuiltItem.MagicTypeId);
            Assert.Equal(item.Name, rebuiltItem.Name);

            // slots
            Assert.Equal<string>(item.EquippableSlots, rebuiltItem.EquippableSlots);

            // stats
            AssertStats.Equal(item.Stats, rebuiltItem.Stats);

            // enchantments
            Assert.Empty(item.Enchantments);
        }
    }
}
