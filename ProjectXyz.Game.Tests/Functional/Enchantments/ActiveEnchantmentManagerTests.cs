using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Testing;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Shared.Framework;
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
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, new Interval<double>(0), true };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, new Interval<double>(10), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, new Interval<double>(0), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, new Interval<double>(5), true };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, new Interval<double>(10), false };
            yield return new object[] { TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, new Interval<double>(11), false };
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
            var activeEnchantmentManager = _fixture
                .ActiveEnchantmentManagerFactory
                .Create();
            activeEnchantmentManager.Add(enchantment);
            Ensures.Equal(
                true,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                "Expecting the enchantment to be contained before elapsing any time.");

            _fixture.ElapsedTimeTriggerSourceMechanic.Update(elapsed);

            AssertEx.Equal(
                contains,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }
        #endregion
    }
}
