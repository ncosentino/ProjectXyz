using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Data.Tests.Enchantments
{
    [Enchantments]
    [DataLayer]
    public class EnchantmentDefinitionFactoryTests
    {
        #region Methods
        [Fact]
        public void EnchantmentDefinitionFactory_CreateEnchantmentDefinition_ExpectedValues()
        {
            var factory = EnchantmentDefinitionFactory.Create();

            var ID = Guid.NewGuid();
            var STAT_ID = Guid.NewGuid();
            var CALCULATION_ID = Guid.NewGuid();
            var TRIGGER_ID = Guid.NewGuid();
            var STATUS_TYPE_ID = Guid.NewGuid();
            const double MINIMUM_VALUE = 123;
            const double MAXIMUM_VALUE = 456;
            const double MINIMUM_DURATION_MILLISECONDS = 1234;
            const double MAXIMUM_DURATION_MILLISECONDS = 5678;

            var result = factory.CreateEnchantmentDefinition(
                ID,
                STAT_ID,
                CALCULATION_ID,
                TRIGGER_ID,
                STATUS_TYPE_ID,
                MINIMUM_VALUE,
                MAXIMUM_VALUE,
                TimeSpan.FromMilliseconds(MINIMUM_DURATION_MILLISECONDS),
                TimeSpan.FromMilliseconds(MAXIMUM_DURATION_MILLISECONDS));

            Assert.Equal(ID, result.Id);
            Assert.Equal(STAT_ID, result.StatId);
            Assert.Equal(CALCULATION_ID, result.CalculationId);
            Assert.Equal(TRIGGER_ID, result.TriggerId);
            Assert.Equal(STATUS_TYPE_ID, result.StatusTypeId);
            Assert.Equal(MINIMUM_VALUE, result.MinimumValue);
            Assert.Equal(MAXIMUM_VALUE, result.MaximumValue);
            Assert.Equal(MINIMUM_DURATION_MILLISECONDS, result.MinimumDuration.TotalMilliseconds);
            Assert.Equal(MAXIMUM_DURATION_MILLISECONDS, result.MaximumDuration.TotalMilliseconds);
        }
        #endregion
    }
}
