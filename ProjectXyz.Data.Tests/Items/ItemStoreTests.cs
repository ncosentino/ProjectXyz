using System;
using System.Collections.Generic;

using Moq;
using Xunit;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Items;

namespace ProjectXyz.Data.Tests.Items
{
    [DataLayer]
    [Items]
    public class ItemStoreTests
    {
        [Fact]
        public void ItemStore_CreateInstance_ExpectedValues()
        {
            var id = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();

            var item = ItemStore.Create(
                id,
                "Item",
                "Resource",
                "Type",
                materialTypeId,
                new[] { "A", "B", "C" });

            Assert.Equal(id, item.Id);
            Assert.Equal("Item", item.Name);
            Assert.Equal("Resource", item.InventoryGraphicResource);
            Assert.Equal(materialTypeId, item.MaterialTypeId);
            Assert.Equal("Type", item.ItemType);
            Assert.Equal(Guid.Empty, item.MagicTypeId);
            Assert.Equal<string>(new[] { "A", "B", "C" }, item.EquippableSlots);
            Assert.Empty(item.Enchantments);
            Assert.Empty(item.SocketedItems);
            Assert.Empty(item.Requirements.Stats);
            Assert.Empty(item.Stats);
        }
    }
}
