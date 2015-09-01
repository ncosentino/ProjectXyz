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
    public class ItemMiscRequirementsTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var raceDefinitionId = Guid.NewGuid();
            var classDefinitionId = Guid.NewGuid();

            // Execute
            var result = ItemMiscRequirements.Create(
                id,
                raceDefinitionId,
                classDefinitionId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(raceDefinitionId, result.RaceDefinitionId);
            Assert.Equal(classDefinitionId, result.ClassDefinitionId);
        }
    }
}
