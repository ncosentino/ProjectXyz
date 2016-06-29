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

        private sealed class TestNodeWrapper : IStatCalculationNodeWrapper
        {
            private readonly IStatCalculationNodeFactoryProvider _statCalculationNodeFactoryProvider;

            public TestNodeWrapper(IStatCalculationNodeFactoryProvider statCalculationNodeFactoryProvider)
            {
                _statCalculationNodeFactoryProvider = statCalculationNodeFactoryProvider;
            }

            public IStatCalculationNode Wrap(
                IIdentifier statDefinitionId,
                IStatCalculationNode statCalculationNode)
            {
                if (statDefinitionId == ALWAYS_OVERRIDDEN_VALUE_STAT_ID)
                {
                    return new ValueStatCalculationNode(111);
                }

                if (statDefinitionId == ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID)
                {
                    return new ValueStatCalculationNode(222);
                }

                if (statDefinitionId == EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID)
                {
                    return _statCalculationNodeFactoryProvider.Factories.Select(factory =>
                    {
                        IStatCalculationNode attemptedNode;
                        return factory.TryCreate(
                            statDefinitionId,
                            "EXPR_OVERRIDE * 2",
                            out attemptedNode)
                            ? attemptedNode
                            : null;
                    })
                    .First(x => x != null);
                }

                return statCalculationNode;
            }
        }
        #endregion

        #region Fields
        private readonly IStatCalculator _statCalculator;
        #endregion

        #region Constructors
        public StatCalculatorTests()
        {
            var stringExpressionEvaluator = new DataTableExpressionEvaluator(new DataTable());

            var statCalculationNodeFactoryProvider = new StatCalculationNodeFactoryProvider();

            var statCalculationNodeWrapper = new TestNodeWrapper(statCalculationNodeFactoryProvider);
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(
                statCalculationNodeFactoryProvider,
                statCalculationNodeWrapper);
            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(
                statCalculationNodeFactory,
                new StringExpressionEvaluatorWrapper(stringExpressionEvaluator, true),
                STAT_DEFINITION_TO_TERM_MAPPING,
                STAT_DEFINITION_TO_CALCULATION_MAPPING);

            statCalculationNodeFactoryProvider.Add(statCalculationValueNodeFactory);
            statCalculationNodeFactoryProvider.Add(statCalculationExpressionNodeFactory);

            var statDefinitionToCalculationLookup = new StatDefinitionToCalculationLookup(
                statCalculationNodeFactory,
                STAT_DEFINITION_TO_CALCULATION_MAPPING);
            _statCalculator = new StatCalculator(statDefinitionToCalculationLookup);
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
            yield return new object[] { ALWAYS_OVERRIDDEN_VALUE_STAT_ID };
            yield return new object[] { ALWAYS_OVERRIDDEN_EXPRESSION_STAT_ID };
            yield return new object[] { EXPRESSION_WITH_OVERRIDDEN_STAT_ID };
            yield return new object[] { EXPRESSION_OVERRIDDEN_BY_OVERRIDE_EXPRESSION_STAT_ID };
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
