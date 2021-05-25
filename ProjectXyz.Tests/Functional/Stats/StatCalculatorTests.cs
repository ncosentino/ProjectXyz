﻿using System;
using System.Collections.Generic;
using System.Linq;

using Jace;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Plugins.Enchantments.Stats.StatExpressions;
using ProjectXyz.Plugins.Features.BoundedStats;
using ProjectXyz.Plugins.Features.BoundedStats.Api;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Plugins.Stats.Calculations;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Math;

using Xunit;

namespace ProjectXyz.Tests.Functional.Stats
{
    public sealed class StatCalculatorTests
    {
        #region Constants
        private static readonly TestFixture TEST_FIXTURE = new TestFixture();
        #endregion

        #region Fields
        private readonly IStatCalculator _statCalculator;
        private readonly IReadOnlyCollection<IStatExpressionInterceptor> _statExpressionInterceptors;
        #endregion

        #region Constructors
        public StatCalculatorTests()
        {
            var jaceCalculationEngine = new CalculationEngine();
            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new GenericExpressionEvaluator(jaceCalculationEngine.Calculate), true);
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
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
                TEST_FIXTURE.StatDefinitionToTermConverter,
                TEST_FIXTURE.StatDefinitionToCalculationConverter);

            _statCalculator = new StatCalculator(statCalculationNodeCreator);
            _statExpressionInterceptors = new[]
            {
                TEST_FIXTURE.StatBoundsExpressionInterceptor,
                TEST_FIXTURE.StatExpressionInterceptor,
            }
            .OrderBy(x => x.Priority)
            .ToArray();
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetEvaluateExpressionTheoryData()
        {
            yield return new object[] { TEST_FIXTURE.Stats.ConstantValue, 123 };
            yield return new object[] { TEST_FIXTURE.Stats.NonDependentExpression, 5 };
            yield return new object[] { TEST_FIXTURE.Stats.ExpressionDependentOnConstantValue, 246 };
            yield return new object[] { TEST_FIXTURE.Stats.ExpressionDependentOnExpression, 49 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ConstantValue, 111 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnConstantValue, 222 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnOverridenConstantValue, 1110 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnOverridenExpression, 2220 };
            yield return new object[] { TEST_FIXTURE.Stats.Bounded.ByLowerLimit, 5 };
            yield return new object[] { TEST_FIXTURE.Stats.Bounded.ByUpperLimit, 0 };
            yield return new object[] { TEST_FIXTURE.Stats.Bounded.ByUpperAndLowerLimit, 5 };
            yield return new object[] { TEST_FIXTURE.Stats.Bounded.ByDependentConstantValue, 123 };
            yield return new object[] { TEST_FIXTURE.Stats.Bounded.ByDependentExpression, 246 };
        }

        public static IEnumerable<object[]> GetUseBaseStatsTheoryData()
        {
            yield return new object[] { TEST_FIXTURE.Stats.ConstantValue };
            yield return new object[] { TEST_FIXTURE.Stats.NonDependentExpression };
            yield return new object[] { TEST_FIXTURE.Stats.ExpressionDependentOnConstantValue };
            yield return new object[] { TEST_FIXTURE.Stats.ExpressionDependentOnExpression };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnOverridenConstantValue };
        }

        public static IEnumerable<object[]> GetOverrideBaseStatsTheoryData()
        {
            yield return new object[] { TEST_FIXTURE.Stats.Override.ConstantValue, 111 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnConstantValue, 222 };
            yield return new object[] { TEST_FIXTURE.Stats.Override.ExpressionDependentOnOverridenExpression, 2220 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetUseBaseStatsTheoryData))]
        private void Calculate_StatPresent_BaseStatUsed(IIdentifier statDefinitionId)
        {
            var expectedValue = new Random().Next(int.MinValue, int.MaxValue);
            var baseStat = new Stat(statDefinitionId, expectedValue);
            var baseStats = baseStat.Yield().ToDictionary(x => x.StatDefinitionId, x => x.Value);
            var result = _statCalculator.Calculate(
                _statExpressionInterceptors,
                baseStats,
                statDefinitionId);
            Assert.Equal(baseStat.Value, result);
        }

        [Theory,
         MemberData(nameof(GetOverrideBaseStatsTheoryData))]
        private void Calculate_StatPresent_BaseStatOverridden(IIdentifier statDefinitionId, double expectedValue)
        {
            var baseValue = new Random().Next(int.MinValue, int.MaxValue);
            var baseStat = new Stat(statDefinitionId, baseValue);
            var baseStats = baseStat.Yield().ToDictionary(x => x.StatDefinitionId, x => x.Value);
            var result = _statCalculator.Calculate(
                _statExpressionInterceptors,
                baseStats,
                statDefinitionId);
            Assert.Equal(expectedValue, result);
        }

        [Theory,
         MemberData(nameof(GetEvaluateExpressionTheoryData))]
        private void Calculate_NoBaseStats_ExpressionEvaluated(
            IIdentifier statDefinitionId,
            double expectedValue)
        {
            var result = _statCalculator.Calculate(
                _statExpressionInterceptors,
                new Dictionary<IIdentifier, double>(),
                statDefinitionId);
            Assert.Equal(expectedValue, result);
        }
        #endregion

        #region Classes
        private sealed class TestFixture
        {
            private static readonly StatDefinitionIds STAT_DEFINITION_IDS = new StatDefinitionIds();

            public StatDefinitionIds Stats { get; } = STAT_DEFINITION_IDS;

            public IStatExpressionInterceptor StatExpressionInterceptor { get; } = new StatExpressionInterceptor(
                (statDefinitionId, expression) =>
                {
                    if (statDefinitionId == STAT_DEFINITION_IDS.Override.ConstantValue)
                    {
                        return "111";
                    }

                    if (statDefinitionId == STAT_DEFINITION_IDS.Override.ExpressionDependentOnConstantValue)
                    {
                        return "222";
                    }

                    if (statDefinitionId == STAT_DEFINITION_IDS.Override.ExpressionDependentOnOverridenExpression)
                    {
                        return "EXPR_OVERRIDE * 2";
                    }

                    return expression;
                },
                1);

            public IStatDefinitionToTermConverter StatDefinitionToTermConverter { get; } = ProjectXyz.Plugins.Stats.StatDefinitionToTermConverter.FromMapping(new Dictionary<IIdentifier, string>()
            {
                { STAT_DEFINITION_IDS.ConstantValue, "STR" },
                { STAT_DEFINITION_IDS.NonDependentExpression, "PHYS_DMG" },
                { STAT_DEFINITION_IDS.ExpressionDependentOnConstantValue, "SIMPLE_EXPRESSION" },
                { STAT_DEFINITION_IDS.Override.ConstantValue, "OVERRIDDEN" },
                { STAT_DEFINITION_IDS.Override.ExpressionDependentOnOverridenConstantValue, "EXPR_OVERRIDE" },
            });

            public IStatDefinitionToCalculationConverter StatDefinitionToCalculationConverter { get; } = ProjectXyz.Plugins.Stats.Calculations.StatDefinitionToCalculationConverter.FromMapping(new Dictionary<IIdentifier, string>()
            {
                { STAT_DEFINITION_IDS.ConstantValue, "123" },
                { STAT_DEFINITION_IDS.NonDependentExpression, "(1 + 2 + 3 + 4) / 2" },
                { STAT_DEFINITION_IDS.ExpressionDependentOnConstantValue, "STR * 2" },
                { STAT_DEFINITION_IDS.ExpressionDependentOnExpression, "(SIMPLE_EXPRESSION - 1) / 5" },

                { STAT_DEFINITION_IDS.Override.ConstantValue, "10" },
                { STAT_DEFINITION_IDS.Override.ExpressionDependentOnConstantValue, "STR * 10" },
                { STAT_DEFINITION_IDS.Override.ExpressionDependentOnOverridenConstantValue, "OVERRIDDEN * 10" },
                { STAT_DEFINITION_IDS.Override.ExpressionDependentOnOverridenExpression, "STR * 10" },
            });

            public IStatExpressionInterceptor StatBoundsExpressionInterceptor { get; } = new StatBoundsExpressionInterceptor(
                new Dictionary<IIdentifier, IStatBounds>()
                {
                    { STAT_DEFINITION_IDS.Bounded.ByLowerLimit, StatBounds.Min("5") },
                    { STAT_DEFINITION_IDS.Bounded.ByUpperLimit, StatBounds.Max("10") },
                    { STAT_DEFINITION_IDS.Bounded.ByUpperAndLowerLimit, new StatBounds("5", "10") },
                    { STAT_DEFINITION_IDS.Bounded.ByDependentConstantValue, new StatBounds("STR", "STR") },
                    { STAT_DEFINITION_IDS.Bounded.ByDependentExpression, new StatBounds("SIMPLE_EXPRESSION", "SIMPLE_EXPRESSION") },
                },
                int.MaxValue);

            public sealed class StatDefinitionIds
            {
                public OverrideStatDefinitionIds Override { get; } = new OverrideStatDefinitionIds();

                public BoundedStatDefinitionIds Bounded { get; } = new BoundedStatDefinitionIds();

                public IIdentifier ConstantValue { get; } = new StringIdentifier("Constant Value Stat");
                public IIdentifier NonDependentExpression { get; } = new StringIdentifier("Non-dependent Expression Stat");
                public IIdentifier ExpressionDependentOnConstantValue { get; } = new StringIdentifier("Single Dependent Expression Stat");
                public IIdentifier ExpressionDependentOnExpression { get; } = new StringIdentifier("Expression Dependent Expression Stat");

                public sealed class BoundedStatDefinitionIds
                {
                    public IIdentifier ByUpperLimit { get; } = new StringIdentifier("Upper Limit Constant Value Stat");

                    public IIdentifier ByLowerLimit { get; } = new StringIdentifier("Lower Limit Constant Value Stat");

                    public IIdentifier ByUpperAndLowerLimit { get; } = new StringIdentifier("Bounded Constant Value Stat");

                    public IIdentifier ByDependentConstantValue { get; } = new StringIdentifier("Bounded Dependent Constant Value Stat");

                    public IIdentifier ByDependentExpression { get; } = new StringIdentifier("Bounded Dependent Expression Stat");
                }

                public sealed class OverrideStatDefinitionIds
                {
                    public IIdentifier ConstantValue { get; } = new StringIdentifier("Always Overridden Value Stat");
                    public IIdentifier ExpressionDependentOnConstantValue { get; } = new StringIdentifier("Always Overridden Expression Stat");
                    public IIdentifier ExpressionDependentOnOverridenConstantValue { get; } = new StringIdentifier("Expression With Overridden Stat");
                    public IIdentifier ExpressionDependentOnOverridenExpression { get; } = new StringIdentifier("Expression Overridden By Override Expression Stat");
                }
            }
        }
        #endregion
    }
}
