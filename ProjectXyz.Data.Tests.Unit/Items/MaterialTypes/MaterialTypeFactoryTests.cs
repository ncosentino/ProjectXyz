using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.Materials;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.MaterialTypes
{
    [DataLayer]
    [Items]
    public class MaterialTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var materialTypeFactory = MaterialTypeFactory.Create();

            // Execute
            var result = materialTypeFactory.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
