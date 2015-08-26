using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Unit
{
    [Enchantments]
    [DataLayer]
    public class ExpressionEnchantmentDefinitionFactoryTests
    {
        #region Methods
        [Fact]
        public void CreateEnchantmentDefinition_ValidArguments_ExpectedValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var expressionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var minimumDuration = TimeSpan.FromSeconds(123);
            var maximumDuration = TimeSpan.FromSeconds(456);

            var factory = ExpressionEnchantmentDefinitionFactory.Create();

            // Execute
            var result = factory.CreateEnchantmentDefinition(
                id,
                enchantmentDefinitionId,
                expressionId,
                statId,
                minimumDuration,
                maximumDuration);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(enchantmentDefinitionId, result.EnchantmentDefinitionId);
            Assert.Equal(statId, result.StatId);;
            Assert.Equal(expressionId, result.ExpressionId);
            Assert.Equal(minimumDuration, result.MinimumDuration);
            Assert.Equal(maximumDuration, result.MaximumDuration);
        }
        #endregion
    }
}
