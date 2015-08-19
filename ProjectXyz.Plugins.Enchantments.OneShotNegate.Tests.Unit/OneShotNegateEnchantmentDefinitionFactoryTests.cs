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
            
            var factory = OneShotNegateEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID);

            // Assert
            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, result.StatId);
        }
        #endregion
    }
}
