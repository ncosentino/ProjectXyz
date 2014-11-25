using System;
using System.Collections.Generic;

using Moq;
using Xunit;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Data.Tests.Items
{
    [DataLayer]
    [Items]
    public class ItemTests
    {
        [Fact]
        public void Item_CreateInstance_DefaultValues()
        {
            var item = Item.Create();

            Assert.NotNull(item.Id);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal("Default", item.Name);
            Assert.Equal("Default", item.MaterialType);
            Assert.Equal("Default", item.ItemType);
            Assert.Equal(Guid.Empty, item.MagicTypeId);
            Assert.Empty(item.EquippableSlots);
            Assert.Empty(item.Enchantments);
            Assert.Empty(item.SocketedItems);
            Assert.Empty(item.Requirements.Stats);
            Assert.Empty(item.Stats);
        }
    }
}
