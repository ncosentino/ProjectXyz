using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class OneShotNegateEnchantmentDefinitionFactoryTests
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

            var factory = OneShotNegateEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID,
                TRIGGER_ID,
                STATUS_TYPE_ID);

            // Assert
            Assert.IsAssignableFrom<IOneShotNegateEnchantmentDefinition>(result);
            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, ((IOneShotNegateEnchantmentDefinition)result).StatId);
            Assert.Equal(TRIGGER_ID, result.TriggerId);
            Assert.Equal(STATUS_TYPE_ID, result.StatusTypeId);
            Assert.Equal(0, result.MinimumDuration.TotalMilliseconds);
            Assert.Equal(0, result.MaximumDuration.TotalMilliseconds);
        }
        #endregion
    }
}
