using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using Xunit;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Tests.Xunit.Assertions.Stats
{
    public static class AssertStats
    {
        #region Methods
        public static void Equal(IStat expected, IStat actual)
        {
            Contract.Requires<ArgumentNullException>(expected != null);
            Contract.Requires<ArgumentNullException>(actual != null);

            Assert.True(
                expected.Id == actual.Id,
                "Expecting stat IDs to be equal.\r\nExpected: " + expected.Id + "\r\nActual: " + actual.Id);
            Assert.True(
                expected.Value == actual.Value,
                "Expecting '" + expected.Id + "' stats to have equal values.\r\nExpected: " + expected.Value + "\r\nActual: " + actual.Value);
        }

        public static void Equal(IStatCollection expected, IStatCollection actual)
        {
            Contract.Requires<ArgumentNullException>(expected != null);
            Contract.Requires<ArgumentNullException>(actual != null);

            Assert.True(
                expected.Count == actual.Count,
                "Expecting same number of stats.\r\nExpected: " + expected.Count + "\r\nActual: " + actual.Count);

            foreach (var stat in expected)
            {
                Assert.True(
                    actual.Contains(stat.Id),
                    "Expecting '" + stat.Id + "' to be contained within the actual stats.");
                Equal(stat, actual[stat.Id]);
            }
        }
        #endregion
    }
}
