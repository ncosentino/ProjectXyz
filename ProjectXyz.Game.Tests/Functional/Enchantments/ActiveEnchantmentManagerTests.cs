using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Testing;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class ActiveEnchantmentManagerTests
    {
        #region Constants
        private static readonly TestData TEST_DATA = new TestData();
        #endregion

        #region Fields
        private readonly TestFixture _fixture;
        #endregion

        #region Constructors
        public ActiveEnchantmentManagerTests()
        {
            _fixture = new TestFixture(TEST_DATA);
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetSingleEnchantmentTheoryData()
        {
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, new Interval<double>(0), true };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, new Interval<double>(10), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatA, new Interval<double>(0), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatA, new Interval<double>(5), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatA, new Interval<double>(10), false };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatA, new Interval<double>(11), false };
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
            _fixture.ActiveEnchantmentManager.Add(enchantment);
            Ensures.Equal(
                true,
                _fixture.ActiveEnchantmentManager.Enchantments.Contains(enchantment),
                "Expecting the enchantment to be contained before elapsing any time.");

            _fixture.ElapsedTimeTriggerSourceMechanic.Update(elapsed);

            AssertEx.Equal(
                contains,
                _fixture.ActiveEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }
        #endregion
    }
}
