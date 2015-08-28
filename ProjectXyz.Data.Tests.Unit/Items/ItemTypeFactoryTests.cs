using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items
{
    [DataLayer]
    [Items]
    public class ItemTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            var itemTypeFactory = ItemTypeFactory.Create();

            // Execute
            var result = itemTypeFactory.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
