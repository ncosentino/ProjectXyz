using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.States;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Enchantments
{
    public sealed class EnchantmentApplierTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;

        static EnchantmentApplierTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { "", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 5 };
            yield return new object[] { "", _testData.Enchantments.Debuffs.StatAAdditiveBaseStat, -5 };
            yield return new object[] { "", _testData.Enchantments.Buffs.StatBAdditiveBaseStat, 5 };
            yield return new object[] { "", _testData.Enchantments.Buffs.StatCAdditiveBaseStat, 10 };
            yield return new object[] { "", _testData.Enchantments.Debuffs.StatCAdditiveBaseStat, 5 };
            yield return new object[] { "", _testData.Enchantments.PreNullifyStatABaseStat, -1 };
            yield return new object[] { "", _testData.Enchantments.PostNullifyStatABaseStat, -1 };
            yield return new object[] { "", _testData.Enchantments.RecursiveStatABaseStat, 0 };
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = 2;
            var halfDuration = 0.5;

            yield return new object[] { "", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 1, 5 };
            yield return new object[] { "", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, doubleDuration, 5 };
            yield return new object[] { "", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, halfDuration, 5 };
            yield return new object[] { "", _testData.Enchantments.BuffsOverTime.StatABaseStat, 1, 10 };
            yield return new object[] { "", _testData.Enchantments.BuffsOverTime.StatABaseStat, doubleDuration, 20 };
            yield return new object[] { "", _testData.Enchantments.BuffsOverTime.StatABaseStat, halfDuration, 5 };
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStats_SingleStatExpectedValue(
            string _,
            IGameObject enchantment,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId]);
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsOverTimeTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStatsOverTime_SingleStatExpectedValue(
            string _,
            IGameObject enchantment,
            double elapsedTurns,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithElapsedTurns(elapsedTurns)
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId]);
        }
    }
}
