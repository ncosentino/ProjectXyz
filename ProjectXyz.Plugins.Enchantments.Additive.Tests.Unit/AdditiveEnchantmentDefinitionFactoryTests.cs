using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Additive.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class AdditiveEnchantmentDefinitionFactoryTests
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

            var factory = AdditiveEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID,
                MINIMUM_VALUE,
                MAXIMUM_VALUE);

            // Assert
            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, ((IAdditiveEnchantmentDefinition)result).StatId);;
            Assert.Equal(MINIMUM_VALUE, ((IAdditiveEnchantmentDefinition)result).MinimumValue);
            Assert.Equal(MAXIMUM_VALUE, ((IAdditiveEnchantmentDefinition)result).MaximumValue);
        }
        #endregion
    }
}
