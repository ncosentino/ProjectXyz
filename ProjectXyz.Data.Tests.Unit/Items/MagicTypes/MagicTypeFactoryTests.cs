using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.MagicTypes;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.MagicTypes
{
    [DataLayer]
    [Items]
    public class MagicTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var magicTypeFactory = MagicTypeFactory.Create();

            // Execute
            var result = magicTypeFactory.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
