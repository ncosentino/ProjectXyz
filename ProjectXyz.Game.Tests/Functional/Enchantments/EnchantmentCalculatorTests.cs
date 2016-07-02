using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentCalculatorTests
    {
        #region Constants
        private static readonly TestFixture FIXTURE = new TestFixture();
        #endregion

        #region Fields
        private readonly EnchantmentCalculator _enchantmentCalculator;
        #endregion

        #region Constructors
        public EnchantmentCalculatorTests()
        {
            var statDefinitionIdToTermMapping = FIXTURE.StatDefinitionIdToTermMapping;
            var statDefinitionIdToCalculationMapping = FIXTURE.StatDefinitionIdToCalculationMapping;

            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new DataTableExpressionEvaluator(new DataTable()), true);

            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(new IStatCalculationNodeFactory[]
            {
                statCalculationValueNodeFactory,
                statCalculationExpressionNodeFactory
            });

            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);

            var statCalculator = new StatCalculator(statCalculationNodeCreator);

            var enchantmentExpressionInterceptorConverter = new EnchantmentExpressionInterceptorConverter();

            var enchantmentExpressionInterceptorFactory = new EnchantmentExpressionInterceptorFactory(statDefinitionIdToTermMapping);

            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                enchantmentExpressionInterceptorConverter);

            _enchantmentCalculator = new EnchantmentCalculator(
                enchantmentExpressionInterceptorFactory,
                enchantmentStatCalculator);
        }
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.StatABuff, 5 };
            yield return new object[] { FIXTURE.Enchantments.StatADebuff, -5 };
            yield return new object[] { FIXTURE.Enchantments.PreNullifyStatA, -1 };
            yield return new object[] { FIXTURE.Enchantments.PostNullifyStatA, -1 };
        }

        private static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatABuff.Yield().Append(FIXTURE.Enchantments.StatABuff), 10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatADebuff.Yield().Append(FIXTURE.Enchantments.StatADebuff), -10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatABuff.Yield().Append(FIXTURE.Enchantments.StatADebuff), 0 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatABuff.Yield().Append(FIXTURE.Enchantments.PreNullifyStatA), 4 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.PostNullifyStatA.Yield().Append(FIXTURE.Enchantments.StatABuff), -1 };
            yield return new object[] { FIXTURE.Stats.StatB, FIXTURE.Enchantments.StatBBuff.Yield().Append(FIXTURE.Enchantments.StatABuff), 5 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData("GetSingleEnchantmentNoBaseStatsTheoryData")]
        private void Calculate_SingleEnchantmentNoBaseStats_ExpectedResult(
            IEnchantment enchantment,
            double expectedResult)
        {
            var baseStats = StatCollection.Empty;
            var result = _enchantmentCalculator.Calculate(
                baseStats,
                enchantment.AsArray(),
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData("GetMultipleEnchantmentsNoBaseStatsTheoryData")]
        private void Calculate_MultipleEnchantmentsNoBaseStats_ExpectedResult(
            IIdentifier statDefinitionId,
            IEnumerable<IEnchantment> enchantments,
            double expectedResult)
        {
            var baseStats = StatCollection.Empty;
            var result = _enchantmentCalculator.Calculate(
                baseStats,
                enchantments.ToArray(),
                statDefinitionId);

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Classes
        private sealed class TestFixture
        {
            private static readonly CalculationPriorities CALC_PRIORITIES = new CalculationPriorities();
            private static readonly StatDefinitionIds STAT_DEFINITION_IDS = new StatDefinitionIds();
            
            public TestFixture()
            {
                Enchantments = new ExampleEnchantments(Stats);
            }

            public ExampleEnchantments Enchantments { get; }

            public StatDefinitionIds Stats { get; } = STAT_DEFINITION_IDS;

            public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToTermMapping { get; } = new Dictionary<IIdentifier, string>()
            {
                { STAT_DEFINITION_IDS.StatA, "STAT_A" },
                { STAT_DEFINITION_IDS.StatB, "STAT_B" },
            };

            public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToCalculationMapping { get; } = new Dictionary<IIdentifier, string>()
            {

            };

            public class ExampleEnchantments
            {
                public ExampleEnchantments(StatDefinitionIds stats)
                {
                    StatADebuff = new ExpressionEnchantment(stats.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle);
                    StatABuff = new ExpressionEnchantment(stats.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);
                    StatBBuff = new ExpressionEnchantment(stats.StatB, "STAT_B + 5", CALC_PRIORITIES.Middle);
                    PreNullifyStatA = new ExpressionEnchantment(stats.StatA, "-1", CALC_PRIORITIES.Lowest);
                    PostNullifyStatA = new ExpressionEnchantment(stats.StatA, "-1", CALC_PRIORITIES.Highest);
                }

                public IEnchantment StatABuff { get; }

                public IEnchantment StatBBuff { get; }

                public IEnchantment StatADebuff { get; }

                public IEnchantment PreNullifyStatA { get; }

                public IEnchantment PostNullifyStatA { get; }
            }

            public class StatDefinitionIds
            {
                public IIdentifier StatA { get; } = new StringIdentifier("Stat A");

                public IIdentifier StatB { get; } = new StringIdentifier("Stat B");
            }

            private class CalculationPriorities
            {
                public ICalculationPriority Lowest { get; } = new CalculationPriority<int>(int.MinValue);

                public ICalculationPriority Middle { get; } = new CalculationPriority<int>(0);

                public ICalculationPriority Highest { get; } = new CalculationPriority<int>(int.MaxValue);
            }
        }
        #endregion
    }
}
