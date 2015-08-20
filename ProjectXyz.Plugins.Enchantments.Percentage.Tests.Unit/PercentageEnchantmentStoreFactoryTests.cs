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
            var statId = Guid.NewGuid();
            const double VALUE = 123;
            var remainingDuration = TimeSpan.FromSeconds(123);

            var factory = PercentageEnchantmentStoreFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentStore(
                id,
                statId,
                VALUE,
                remainingDuration);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(statId, result.StatId);
            Assert.Equal(VALUE, result.Value);
            Assert.Equal(remainingDuration.TotalMilliseconds, result.RemainingDuration.TotalMilliseconds);
        }
        #endregion
    }
}
