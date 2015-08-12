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
            var enchantmentTypeId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var triggerId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();

            var factory = OneShotNegateEnchantmentStoreFactory.Create(enchantmentTypeId);

            // Execute
            var result = factory.CreateEnchantmentStore(
                id,
                statId,
                triggerId,
                statusTypeId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(enchantmentTypeId, result.EnchantmentTypeId);
            Assert.Equal(statId, result.StatId);
            Assert.Equal(triggerId, result.TriggerId);
            Assert.Equal(statusTypeId, result.StatusTypeId);
            Assert.Equal(0, result.RemainingDuration.TotalMilliseconds);
        }
        #endregion
    }
}
