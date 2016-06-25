using System;
using System.Collections.Generic;
using System.Data;
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

        private static readonly IReadOnlyDictionary<IIdentifier, string> STAT_DEFINITION_TO_TERM_MAPPING = new Dictionary<IIdentifier, string>()
        {
            { CONSTANT_VALUE_STAT_ID, "STR" },
            { NON_DEPENDENT_EXPRESSION_STAT_ID, "PHYS_DMG" },
            { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, "SIMPLE_EXPRESSION" }
        };

        private static readonly IReadOnlyDictionary<IIdentifier, string> STAT_DEFINITION_TO_CALCULATION_MAPPING = new Dictionary<IIdentifier, string>()
        {
            { CONSTANT_VALUE_STAT_ID, "123" },
            { NON_DEPENDENT_EXPRESSION_STAT_ID, "(1 + 2 + 3 + 4) / 2" },
            { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, "STR * 2" },
            { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID, "(SIMPLE_EXPRESSION - 1) / 5" },
        };
        #endregion

        #region Fields
        private readonly IStatCalculator _statCalculator;
        private readonly IStatCollectionFactory _statCollectionFactory;
        #endregion

        #region Constructors
        public StatCalculatorTests()
        {
            _statCollectionFactory = new StatCollectionFactory();

            var stringExpressionEvaluator = new DataTableExpressionEvaluator(new DataTable());
            var statCalculationNodeFactory = new StatCalculationNodeFactory(new StringExpressionEvaluatorWrapper(stringExpressionEvaluator, true));
            var statDefinitionToCalculationNodeFactory = new StatDefinitionToCalculationNodeFactory(statCalculationNodeFactory);
            var statDefinitionToCalculationNodeMapping = statDefinitionToCalculationNodeFactory.CreateMapping(
                STAT_DEFINITION_TO_TERM_MAPPING,
                STAT_DEFINITION_TO_CALCULATION_MAPPING);
            _statCalculator = new StatCalculator(statDefinitionToCalculationNodeMapping);
        }
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetEvaluateExpressionTheoryData()
        {
            yield return new object[] { CONSTANT_VALUE_STAT_ID, 123 };
            yield return new object[] { NON_DEPENDENT_EXPRESSION_STAT_ID, 5 };
            yield return new object[] { SINGLE_DEPENDENT_EXPRESSION_STAT_ID, 246 };
            yield return new object[] { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID, 49 };
        }

        private static IEnumerable<object[]> GetUseBaseStatsTheoryData()
        {
            yield return new object[] { CONSTANT_VALUE_STAT_ID };
            yield return new object[] { NON_DEPENDENT_EXPRESSION_STAT_ID };
            yield return new object[] { SINGLE_DEPENDENT_EXPRESSION_STAT_ID };
            yield return new object[] { EXPRESSION_DEPENDENT_EXPRESSION_STAT_ID };
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
            var baseStats = _statCollectionFactory.Create(baseStat);
            var result = _statCalculator.Calculate(
                baseStat.StatDefinitionId,
                baseStats);
            Assert.Equal(baseStat.Value, result);
        }
        
        [Theory,
         MemberData("GetEvaluateExpressionTheoryData")]
        private void Calculate_NoBaseStats_ExpressionEvaluated(
            IIdentifier statDefinitionId,
            double expectedValue)
        {
            var result = _statCalculator.Calculate(
                statDefinitionId,
                StatCollection.Empty);
            Assert.Equal(expectedValue, result);
        }
        #endregion
    }
}
