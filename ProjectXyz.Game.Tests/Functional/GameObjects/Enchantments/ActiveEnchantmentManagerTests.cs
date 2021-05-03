using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Testing;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Features.TurnBased;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Enchantments
{
    public sealed class ActiveEnchantmentManagerTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;

        static ActiveEnchantmentManagerTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
        }

        public static IEnumerable<object[]> GetSingleEnchantmentTheoryData()
        {
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, true };
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 10, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 11, false };
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentTheoryData))]
        private void ElapseTime_SingleEnchantment_ContainsExpectedValue(
            IGameObject enchantment,
            double elapsedTurns,
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

            var turnInfo = new TurnInfo(
                new IGameObject[] { },
                elapsedTurns,
                true);
            _fixture.ElapsedTurnsTriggerSourceMechanic.Update(turnInfo);

            AssertEx.Equal(
                contains,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }
    }
}
