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
    public class EquipSlotTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            var equipSlotTypeFactory = EquipSlotTypeFactory.Create();

            // Execute
            var result = equipSlotTypeFactory.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
