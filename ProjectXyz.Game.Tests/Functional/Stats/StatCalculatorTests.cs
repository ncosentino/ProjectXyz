using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Stats
{
    public sealed class StatCalculatorTests
    {
        #region Constants
        private static readonly IIdentifier CONSTANT_VALUE_STAT_ID = new StringIdentifier("Constant Value Stat");
        private static readonly IIdentifier NON_DEPENDENT_EXPRESSION_STAT_ID = new StringIdentifier("Non-dependent Expression Stat");
        private static readonly IIdentifier SINGLE_DEPENDENT_EXPRESSION_STAT_ID = new StringIdentifier("Single Dependent Expression Stat");
        private static readonly IIdentifier EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID = new StringIdentifier("Expression Dependent Expression Stat");

        private static readonly IIdentifier ALWAYS_OVERRIDDEN_VALUE_STAT_ID = new StringIdentifier("Always Overridden Value Stat");
        private static readonly IIdentifier ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID = new StringIdentifier("Always Overridden Expression Stat");
        private static readonly IIdentifier EXPRESSION_WITH_OVERRIDDEN_STAT_ID = new StringIdentifier("Expression With Overridden Stat");
        private static readonly IIdentifier EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID = new StringIdentifier("Expression Overridden By Override Expression Stat");

        private static readonly IReadOnlyDictionary<IIdentifier, string> STAT_DEFINITION_TO_TERM_MAPPING = new Dictionary<IIdentifier, string>()
        {
            { CONSTANT_VALUE_STAT_ID, "STR" },
            { NON_DEPENDENT_EXPRESSION_STAT_ID, "PHYS_DMG" },
            { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, "SIMPLE_EXPRESSION" },
            { ALWAYS_OVERRIDDEN_VALUE_STAT_ID, "OVERRIDDEN" },
            { EXPRESSION_WITH_OVERRIDDEN_STAT_ID, "EXPR_OVERRIDE" },
        };

        private static readonly IReadOnlyDictionary<IIdentifier, string> STAT_DEFINITION_TO_CALCULATION_MAPPING = new Dictionary<IIdentifier, string>()
        {
            { CONSTANT_VALUE_STAT_ID, "123" },
            { NON_DEPENDENT_EXPRESSION_STAT_ID, "(1 + 2 + 3 + 4) / 2" },
            { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, "STR * 2" },
            { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID, "(SIMPLE_EXPRESSION - 1) / 5" },

            { ALWAYS_OVERRIDDEN_VALUE_STAT_ID, "10" },
            { ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID, "STR * 10" },
            { EXPRESSION_WITH_OVERRIDDEN_STAT_ID, "OVERRIDDEN * 10" },
            { EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID, "STR * 10" },
        };
        #endregion

        #region Fields
        private readonly IStatCalculator _statCalculator;
        private readonly IStatExpressionInterceptor _statExpressionInterceptor;
        #endregion

        #region Constructors
        public StatCalculatorTests()
        {
            var stringExpressionEvaluator = new DataTableExpressionEvaluator(new DataTable());
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(new StringExpressionEvaluatorWrapper(stringExpressionEvaluator, true));
            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(new IStatCalculationNodeFactory[]
            {
                statCalculationValueNodeFactory,
                statCalculationExpressionNodeFactory,
            });

            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                STAT_DEFINITION_TO_TERM_MAPPING,
                STAT_DEFINITION_TO_CALCULATION_MAPPING);

            _statCalculator = new StatCalculator(statCalculationNodeCreator);
            _statExpressionInterceptor = new StatExpressionInterceptor((statDefinitionId, expression) =>
            {
                if (statDefinitionId == ALWAYS_OVERRIDDEN_VALUE_STAT_ID)
                {
                    return "111";
                }

                if (statDefinitionId == ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID)
                {
                    return "222";
                }

                if (statDefinitionId == EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID)
                {
                    return "EXPR_OVERRIDE * 2";
                }

                return expression;
            });
        }
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetEvaluateExpressionTheoryData()
        {
            yield return new object[] { CONSTANT_VALUE_STAT_ID, 123 };
            yield return new object[] { NON_DEPENDENT_EXPRESSION_STAT_ID, 5 };
            yield return new object[] { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, 246 };
            yield return new object[] { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID, 49 };
            yield return new object[] { ALWAYS_OVERRIDDEN_VALUE_STAT_ID, 111 };
            yield return new object[] { ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID, 222 };
            yield return new object[] { EXPRESSION_WITH_OVERRIDDEN_STAT_ID, 1110 };
            yield return new object[] { EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID, 2220 };
        }

        private static IEnumerable<object[]> GetUseBaseStatsTheoryData()
        {
            yield return new object[] { CONSTANT_VALUE_STAT_ID };
            yield return new object[] { NON_DEPENDENT_EXPRESSION_STAT_ID };
            yield return new object[] { SINGLE_DEPENDENT_EXPRESSION_STAT_ID };
            yield return new object[] { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID };
            yield return new object[] { EXPRESSION_WITH_OVERRIDDEN_STAT_ID };
        }

        private static IEnumerable<object[]> GetOverrideBaseStatsTheoryData()
        {
            yield return new object[] { ALWAYS_OVERRIDDEN_VALUE_STAT_ID, 111 };
            yield return new object[] { ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID, 222 };
            yield return new object[] { EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID, 2220 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData("GetUseBaseStatsTheoryData")]
        private void Calculate_StatPresent_BaseStatUsed(IIdentifier statDefinitionId)
        {
            var rng = new RandomNumberGenerator(new Random());
            var expectedValue = rng.NextInRange(int.MinValue, int.MaxValue);
            var baseStat = new Stat(statDefinitionId, expectedValue);
            var baseStats = new StatCollectionFactory().Create(baseStat);
            var result = _statCalculator.Calculate(
                _statExpressionInterceptor,
                baseStats,
                statDefinitionId);
            Assert.Equal(baseStat.Value, result);
        }

        [Theory,
         MemberData("GetOverrideBaseStatsTheoryData")]
        private void Calculate_StatPresent_BaseStatOverridden(IIdentifier statDefinitionId, double expectedValue)
        {
            var rng = new RandomNumberGenerator(new Random());
            var baseValue = rng.NextInRange(int.MinValue, int.MaxValue);
            var baseStat = new Stat(statDefinitionId, baseValue);
            var baseStats = new StatCollectionFactory().Create(baseStat);
            var result = _statCalculator.Calculate(
                _statExpressionInterceptor,
                baseStats,
                statDefinitionId);
            Assert.Equal(expectedValue, result);
        }

        [Theory,
         MemberData("GetEvaluateExpressionTheoryData")]
        private void Calculate_NoBaseStats_ExpressionEvaluated(
            IIdentifier statDefinitionId,
            double expectedValue)
        {
            var result = _statCalculator.Calculate(
                _statExpressionInterceptor,
                StatCollection.Empty,
                statDefinitionId);
            Assert.Equal(expectedValue, result);
        }
        #endregion
    }
}
