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
    public class EquipSlotTypeTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            // Execute
            var result = EquipSlotType.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
