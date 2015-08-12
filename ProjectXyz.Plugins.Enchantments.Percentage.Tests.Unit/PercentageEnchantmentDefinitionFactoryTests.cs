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
            var TRIGGER_ID = Guid.NewGuid();
            var STATUS_TYPE_ID = Guid.NewGuid();
            const double MINIMUM_VALUE = 123;
            const double MAXIMUM_VALUE = 456;
            const double MINIMUM_DURATION_MILLISECONDS = 1234;
            const double MAXIMUM_DURATION_MILLISECONDS = 5678;

            var factory = PercentageEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID,
                TRIGGER_ID,
                STATUS_TYPE_ID,
                MINIMUM_VALUE,
                MAXIMUM_VALUE,
                TimeSpan.FromMilliseconds(MINIMUM_DURATION_MILLISECONDS),
                TimeSpan.FromMilliseconds(MAXIMUM_DURATION_MILLISECONDS));

            // Assert
            Assert.IsAssignableFrom<IPercentageEnchantmentDefinition>(result);
            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, ((IPercentageEnchantmentDefinition)result).StatId);
            Assert.Equal(TRIGGER_ID, result.TriggerId);
            Assert.Equal(STATUS_TYPE_ID, result.StatusTypeId);
            Assert.Equal(MINIMUM_VALUE, ((IPercentageEnchantmentDefinition)result).MinimumValue);
            Assert.Equal(MAXIMUM_VALUE, ((IPercentageEnchantmentDefinition)result).MaximumValue);
            Assert.Equal(MINIMUM_DURATION_MILLISECONDS, result.MinimumDuration.TotalMilliseconds);
            Assert.Equal(MAXIMUM_DURATION_MILLISECONDS, result.MaximumDuration.TotalMilliseconds);
        }
        #endregion
    }
}
