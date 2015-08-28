using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.EquipSlots;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.EquipSlots
{
    [DataLayer]
    [Items]
    public class ItemTypeEquipSlotTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var equipSlotTypeId = Guid.NewGuid();

            var itemStoreFactory = ItemTypeEquipSlotTypeFactory.Create();

            // Execute
            var result = itemStoreFactory.Create(
                id,
                itemTypeId,
                equipSlotTypeId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(itemTypeId, result.ItemTypeId);
            Assert.Equal(equipSlotTypeId, result.EquipSlotTypeId);
        }
    }
}
