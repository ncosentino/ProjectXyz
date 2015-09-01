using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Stats
{
    [Stats]
    [DataLayer]
    public class StatFactoryTests
    {
        #region Methods
        [Fact]
        public void CreateStat_ValidArguments_ExpectedValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var statDefinitionId = Guid.NewGuid();
            const double VALUE = 123;

            var factory = StatFactory.Create();

            // Execute
            var result = factory.Create(
                id,
                statDefinitionId,
                VALUE);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(statDefinitionId, result.StatDefinitionId);
            Assert.Equal(VALUE, result.Value);
        }
        #endregion
    }
}
