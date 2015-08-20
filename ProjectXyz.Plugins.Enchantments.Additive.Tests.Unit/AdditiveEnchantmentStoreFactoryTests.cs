using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Additive.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class AdditiveEnchantmentStoreFactoryTests
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

            var factory = AdditiveEnchantmentStoreFactory.Create();

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
