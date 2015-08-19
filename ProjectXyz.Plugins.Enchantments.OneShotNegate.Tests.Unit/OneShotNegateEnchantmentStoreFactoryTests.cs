using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class OneShotNegateEnchantmentStoreFactoryTests
    {
        #region Methods
        [Fact]
        public void CreateEnchantmentStore_ValidParameters_ExpectedValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var statId = Guid.NewGuid();

            var factory = OneShotNegateEnchantmentStoreFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentStore(
                id,
                statId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(statId, result.StatId);
        }
        #endregion
    }
}
