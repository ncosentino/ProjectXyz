using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using Xunit;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Tests.Xunit.Assertions.Enchantments
{
    public static class AssertEnchantments
    {
        #region Methods
        public static void Equal(IEnchantment expected, IEnchantment actual)
        {
            Contract.Requires<ArgumentNullException>(expected != null);
            Contract.Requires<ArgumentNullException>(actual != null);

            Equal(expected, actual, -1);
        }

        public static void Equal(IEnchantmentCollection expected, IEnchantmentCollection actual)
        {
            Contract.Requires<ArgumentNullException>(expected != null);
            Contract.Requires<ArgumentNullException>(actual != null);

            Assert.True(
                expected.Count == actual.Count,
                "Expecting same number of enchantments.\r\nExpected: " + expected.Count + "\r\nActual: " + actual.Count);

            for (int i = 0; i < expected.Count; ++i)
            {
                Equal(expected[i], actual[i]);
            }
        }

        private static void Equal(IEnchantment expected, IEnchantment actual, int index = -1)
        {
            Contract.Requires<ArgumentNullException>(expected != null);
            Contract.Requires<ArgumentNullException>(actual != null);

            string enchantmentLabel = "enchantment";
            if (index >= 0)
            {
                enchantmentLabel += " (index " + index + ")";
            }

            Assert.True(
                expected.CalculationId == actual.CalculationId,
                "Expecting " + enchantmentLabel + " calculation IDs to be equal.\r\nExpected: " + expected.CalculationId + "\r\nActual: " + actual.CalculationId);
            Assert.True(
                expected.RemainingDuration == actual.RemainingDuration,
                "Expecting " + enchantmentLabel + " remaining durations to be equal.\r\nExpected: " + expected.RemainingDuration + "\r\nActual: " + actual.RemainingDuration);
            Assert.True(
                expected.StatId == actual.StatId,
                "Expecting " + enchantmentLabel + " stat IDs to be equal.\r\nExpected: " + expected.StatId + "\r\nActual: " + actual.StatId);
            Assert.True(
                expected.Value == actual.Value,
                "Expecting " + enchantmentLabel + " values to be equal.\r\nExpected: " + expected.Value + "\r\nActual: " + actual.Value);
        }
        #endregion
    }
}