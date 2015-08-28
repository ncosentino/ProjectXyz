using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.Affixes;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.Affixes
{
    [DataLayer]
    [Items]
    public class ItemAffixDefinitionFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            const bool IS_PREFIX = true;
            const int MINIMUM_LEVEL = 123;
            const int MAXIMUM_LEVEL = 456;

            var itemAffixDefinitionFactory = ItemAffixDefinitionFactory.Create();

            // Execute
            var result = itemAffixDefinitionFactory.Create(
                id,
                nameStringResourceId,
                IS_PREFIX,
                MINIMUM_LEVEL,
                MAXIMUM_LEVEL);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
            Assert.Equal(IS_PREFIX, result.IsPrefix);
            Assert.Equal(MINIMUM_LEVEL, result.MinimumLevel);
            Assert.Equal(MAXIMUM_LEVEL, result.MaximumLevel);
        }
    }
}
