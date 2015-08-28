using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.Requirements;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.Requirements
{
    [DataLayer]
    [Items]
    public class ItemStatRequirementsTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var statId = Guid.NewGuid();

            // Execute
            var result = ItemStatRequirements.Create(
                id,
                itemId,
                statId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(itemId, result.ItemId);
            Assert.Equal(statId, result.StatId);
        }
    }
}
