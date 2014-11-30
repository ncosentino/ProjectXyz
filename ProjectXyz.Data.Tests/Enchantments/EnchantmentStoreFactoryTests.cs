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
    public class EnchantmentStoreFactoryTests
    {
        #region Methods
        [Fact]
        public void EnchantmentStoreFactory_CreateEnchantmentStore_ExpectedValues()
        {
            var factory = EnchantmentStoreFactory.Create();

            var ID = Guid.NewGuid();
            var STAT_ID = Guid.NewGuid();
            var CALCULATION_ID = Guid.NewGuid();
            var TRIGGER_ID = Guid.NewGuid();
            var STATUS_TYPE_ID = Guid.NewGuid();
            const double VALUE = 123;
            const double DURATION_MILLISECONDS = 1234;

            var result = factory.CreateEnchantmentStore(
                STAT_ID,
                CALCULATION_ID,
                TRIGGER_ID,
                STATUS_TYPE_ID,
                TimeSpan.FromMilliseconds(DURATION_MILLISECONDS),
                VALUE);

            Assert.Equal(STAT_ID, result.StatId);
            Assert.Equal(CALCULATION_ID, result.CalculationId);
            Assert.Equal(TRIGGER_ID, result.TriggerId);
            Assert.Equal(STATUS_TYPE_ID, result.StatusTypeId);
            Assert.Equal(VALUE, result.Value);
            Assert.Equal(DURATION_MILLISECONDS, result.RemainingDuration.TotalMilliseconds);
        }
        #endregion
    }
}
