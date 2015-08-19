using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class PercentageEnchantmentDefinitionFactoryTests
    {
        #region Methods
        [Fact]
        public void CreateEnchantmentDefinition_ValidArguments_ExpectedValues()
        {
            // Setup
            var ID = Guid.NewGuid();
            var STAT_ID = Guid.NewGuid();
            const double MINIMUM_VALUE = 123;
            const double MAXIMUM_VALUE = 456;

            var factory = PercentageEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID,
                MINIMUM_VALUE,
                MAXIMUM_VALUE);

            // Assert
            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, ((IPercentageEnchantmentDefinition)result).StatId);;
            Assert.Equal(MINIMUM_VALUE, ((IPercentageEnchantmentDefinition)result).MinimumValue);
            Assert.Equal(MAXIMUM_VALUE, ((IPercentageEnchantmentDefinition)result).MaximumValue);
        }
        #endregion
    }
}
