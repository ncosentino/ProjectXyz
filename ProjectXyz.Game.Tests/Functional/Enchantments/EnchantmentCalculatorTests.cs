using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
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

            var enchantmentStatCalculator = new StatCalculationWrapper(
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
            yield return new object[] { FIXTURE.Enchantments.StatBuff, 5 };
            yield return new object[] { FIXTURE.Enchantments.StatDebuff, -5 };
            yield return new object[] { FIXTURE.Enchantments.PreNullifyStat, -1 };
            yield return new object[] { FIXTURE.Enchantments.PostNullifyStat, -1 };
        }

        private static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatBuff.Yield().Append(FIXTURE.Enchantments.StatBuff), 10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatDebuff.Yield().Append(FIXTURE.Enchantments.StatDebuff), -10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatBuff.Yield().Append(FIXTURE.Enchantments.StatDebuff), 0 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.StatBuff.Yield().Append(FIXTURE.Enchantments.PreNullifyStat), 4 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.PostNullifyStat.Yield().Append(FIXTURE.Enchantments.StatBuff), -1 };
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
                { STAT_DEFINITION_IDS.StatA, "STAT_A" }
            };

            public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToCalculationMapping { get; } = new Dictionary<IIdentifier, string>()
            {

            };

            public class ExampleEnchantments
            {
                public ExampleEnchantments(StatDefinitionIds stats)
                {
                    StatDebuff = new ExpressionEnchantment(stats.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle);
                    StatBuff = new ExpressionEnchantment(stats.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);
                    PreNullifyStat = new ExpressionEnchantment(stats.StatA, "-1", CALC_PRIORITIES.Lowest);
                    PostNullifyStat = new ExpressionEnchantment(stats.StatA, "-1", CALC_PRIORITIES.Highest);
                }

                public IEnchantment StatBuff { get; }

                public IEnchantment StatDebuff { get; }

                public IEnchantment PreNullifyStat { get; }

                public IEnchantment PostNullifyStat { get; }
            }

            public class StatDefinitionIds
            {
                public IIdentifier StatA { get; } = new StringIdentifier("Stat A");
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

    public interface ICalculationPriority : IComparable
    {
    }

    public interface ICalculationPriority<T> :
        ICalculationPriority,
        IComparable<ICalculationPriority<T>>
        where T : IComparable<T>
    {
        T Value { get; }
    }

    public sealed class CalculationPriority<T> :
        ICalculationPriority<T>
        where T : IComparable<T>
    {
        public CalculationPriority(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public int CompareTo(ICalculationPriority<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var casted = obj as ICalculationPriority<T>;
            if (casted != null)
            {
                return CompareTo(casted);
            }

            throw new ArgumentException($"Object is not of type '{GetType()}'.");
        }
    }

    public interface IEnchantment
    {
        IIdentifier StatDefinitionId { get; }

        ICalculationPriority CalculationPriority { get; }
    }

    public interface IExpressionEnchantment : IEnchantment
    {
        string Expression { get; }
    }

    public sealed class ExpressionEnchantment : IExpressionEnchantment
    {
        public ExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority)
        {
            StatDefinitionId = statDefinitionId;
            Expression = expression;
            CalculationPriority = calculationPriority;
        }

        public IIdentifier StatDefinitionId { get; }

        public ICalculationPriority CalculationPriority { get; }

        public string Expression { get; }

        public override string ToString()
        {
            return $"Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}\r\n\tCalculation Priority:{CalculationPriority}\r\n\tExpression={Expression}";
        }
    }
    
    public interface IExpressionTermToStateValueConverter
    {
        string Convert(string expression);
    }

    public sealed class ExpressionTermToStateValueConverter : IExpressionTermToStateValueConverter
    {
        private readonly IReadOnlyDictionary<string, Tuple<IIdentifier, IIdentifier>> _termToStateIdMapping;

        public ExpressionTermToStateValueConverter(IReadOnlyDictionary<string, Tuple<IIdentifier, IIdentifier>> termToStateIdMapping)
        {
            _termToStateIdMapping = termToStateIdMapping;
        }

        public string Convert(string expression)
        {
            // FIXME: use a state-value lookup
            return _termToStateIdMapping.Aggregate(
                expression,
                (current, kvp) => current.Replace(kvp.Key, "(123)"));
        }
    }

    public sealed class EnchantmentCalculator
    {
        private readonly IEnchantmentExpressionInterceptorFactory _enchantmentExpressionInterceptorFactory;
        private readonly IEnchantmentStatCalculator _enchantmentStatCalculator;

        public EnchantmentCalculator(
            IEnchantmentExpressionInterceptorFactory enchantmentExpressionInterceptorFactory,
            IEnchantmentStatCalculator enchantmentStatCalculator)
        {
            _enchantmentExpressionInterceptorFactory = enchantmentExpressionInterceptorFactory;
            _enchantmentStatCalculator = enchantmentStatCalculator;
        }

        public double Calculate(
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments,
            IIdentifier statDefinitionId)
        {
            var interceptor = _enchantmentExpressionInterceptorFactory.Create(enchantments);
            var value = _enchantmentStatCalculator.Calculate(
                interceptor,
                baseStats,
                statDefinitionId);
            return value;
        }
    }

    public interface IEnchantmentStatCalculator
    {
        double Calculate(
            IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IIdentifier statDefinitionId);
    }

    public sealed class StatCalculationWrapper : IEnchantmentStatCalculator
    {
        private readonly IStatCalculator _statCalculator;
        private readonly IEnchantmentExpressionInterceptorConverter _enchantmentExpressionInterceptorConverter;

        public StatCalculationWrapper(
            IStatCalculator statCalculator,
            IEnchantmentExpressionInterceptorConverter enchantmentExpressionInterceptorConverter)
        {
            _statCalculator = statCalculator;
            _enchantmentExpressionInterceptorConverter = enchantmentExpressionInterceptorConverter;
        }

        public double Calculate(
            IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IIdentifier statDefinitionId)
        {
            var statExpressionInterceptor = _enchantmentExpressionInterceptorConverter.Convert(enchantmentExpressionInterceptor);
            var value = _statCalculator.Calculate(
                statExpressionInterceptor,
                baseStats,
                statDefinitionId);
            return value;
        }
    }

    public interface IEnchantmentExpressionInterceptorConverter
    {
        IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);
    }

    public sealed class EnchantmentExpressionInterceptorConverter : IEnchantmentExpressionInterceptorConverter
    {
        public IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor)
        {
            return new StatExpressionInterceptor(enchantmentExpressionInterceptor.Intercept);
        }
    }

    public interface IEnchantmentExpressionInterceptor
    {
        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }

    public interface IEnchantmentExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IEnchantment> enchantments);
    }

    public sealed class EnchantmentExpressionInterceptorFactory : IEnchantmentExpressionInterceptorFactory
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public EnchantmentExpressionInterceptorFactory(IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IEnchantment> enchantments)
        {
            var statDefinitionToEnchantmentMapping = enchantments
                .TakeTypes<IExpressionEnchantment>()
                .GroupBy(
                    x => x.StatDefinitionId,
                    x => x)
                .ToDictionary(
                    x => x.Key,
                    x => (IReadOnlyCollection<IExpressionEnchantment>)x.Select(g => g).OrderBy(e => e.CalculationPriority).ToArray());

            var interceptor = new ExpressionEnchantmentExpressionInterceptor(
                _statDefinitionIdToTermMapping,
                statDefinitionToEnchantmentMapping);
            return interceptor;
        }
    }

    public sealed class ExpressionEnchantmentExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> _statDefinitionToEnchantmentMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public ExpressionEnchantmentExpressionInterceptor(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> statDefinitionToEnchantmentMapping)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToEnchantmentMapping = statDefinitionToEnchantmentMapping;
        }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            var applicableEnchantments = _statDefinitionToEnchantmentMapping.GetValueOrDefault(
                statDefinitionId,
                () => new IExpressionEnchantment[0]);

            var term = _statDefinitionIdToTermMapping[statDefinitionId];

            expression = applicableEnchantments.Aggregate(
                expression,
                (current, enchantment) => enchantment.Expression.Replace(
                    term,
                    $"({current})"));
            return expression;
        }
    }
}
