using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class ExpressionEnchantmentStoreFactoryTests
    {
        #region Methods
        [Fact]
        public void CreateEnchantmentStore_ValidParameters_ExpectedValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string EXPRESSION = "this is the expression";
            var remainingDuration = TimeSpan.FromSeconds(123);

            var factory = ExpressionEnchantmentStoreFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentStore(
                id,
                statId,
                EXPRESSION,
                remainingDuration);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(statId, result.StatId);
            Assert.Equal(EXPRESSION, result.Expression);
            Assert.Equal(remainingDuration.TotalMilliseconds, result.RemainingDuration.TotalMilliseconds);
        }
        #endregion
    }
}
