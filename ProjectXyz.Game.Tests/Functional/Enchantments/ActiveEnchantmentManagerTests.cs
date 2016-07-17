using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Testing;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class ActiveEnchantmentManagerTests
    {
        #region Constants
        private static readonly TestFixture FIXTURE = new TestFixture();
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetSingleEnchantmentTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, new Interval<double>(0), true };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, new Interval<double>(10), true };
            yield return new object[] { FIXTURE.Enchantments.BuffsThatExpire.StatA, new Interval<double>(0), true };
            yield return new object[] { FIXTURE.Enchantments.BuffsThatExpire.StatA, new Interval<double>(5), true };
            yield return new object[] { FIXTURE.Enchantments.BuffsThatExpire.StatA, new Interval<double>(10), false };
            yield return new object[] { FIXTURE.Enchantments.BuffsThatExpire.StatA, new Interval<double>(11), false };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetSingleEnchantmentTheoryData))]
        private void ElapseTime_SingleEnchantment_ContainsExpectedValue(
            IEnchantment enchantment,
            IInterval elapsed,
            bool contains)
        {
            FIXTURE.ActiveEnchantmentManager.Add(enchantment);
            Ensures.Equal(
                true,
                FIXTURE.ActiveEnchantmentManager.Enchantments.Contains(enchantment),
                "Expecting the enchantment to be contained before elapsing any time.");

            FIXTURE.ElapsedTimeTriggerSourceMechanic.Update(elapsed);

            AssertEx.Equal(
                contains,
                FIXTURE.ActiveEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }
        #endregion
    }
}
