using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Data.Tests.Stats
{
    [Stats]
    [DataLayer]
    public class StatFactoryTests
    {
        #region Methods
        [Fact]
        public void StatFactory_CreateStat_ExpectedValues()
        {
            var factory = StatFactory.Create();

            var STAT_ID = Guid.NewGuid();
            const double VALUE = 123;

            var result = factory.CreateStat(
                STAT_ID,
                VALUE);

            Assert.Equal(STAT_ID, result.Id);
            Assert.Equal(VALUE, result.Value);
        }
        #endregion
    }
}
