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
    public class ItemMiscRequirementsFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var raceDefinitionId = Guid.NewGuid();
            var classDefinitionid = Guid.NewGuid();

            var itemMiscRequirementsFactory = ItemMiscRequirementsFactory.Create();

            // Execute
            var result = itemMiscRequirementsFactory.Create(
                id,
                raceDefinitionId,
                classDefinitionid);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(raceDefinitionId, result.RaceDefinitionId);
            Assert.Equal(classDefinitionid, result.ClassDefinitionId);
        }
    }
}
