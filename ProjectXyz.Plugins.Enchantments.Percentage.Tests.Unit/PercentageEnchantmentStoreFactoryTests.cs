using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class PercentageEnchantmentStoreFactoryTests
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
            const double VALUE = 123;
            const double DURATION_MILLISECONDS = 1234;

            var factory = PercentageEnchantmentStoreFactory.Create(enchantmentTypeId);

            // Execute
            var result = factory.CreateEnchantmentStore(
                id,
                statId,
                triggerId,
                statusTypeId,
                TimeSpan.FromMilliseconds(DURATION_MILLISECONDS),
                VALUE);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(enchantmentTypeId, result.EnchantmentTypeId);
            Assert.Equal(statId, result.StatId);
            Assert.Equal(triggerId, result.TriggerId);
            Assert.Equal(statusTypeId, result.StatusTypeId);
            Assert.Equal(VALUE, result.Value);
            Assert.Equal(DURATION_MILLISECONDS, result.RemainingDuration.TotalMilliseconds);
        }
        #endregion
    }
}
