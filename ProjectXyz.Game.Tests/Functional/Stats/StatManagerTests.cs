using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework.Entities;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Stats
{
    public sealed class StatManagerTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;

        static StatManagerTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
        }

        public static IEnumerable<object[]> GetNoStateTestData()
        {
            yield return new object[] { _testData.Stats.DefinitionIds.StatA, -100 };
            yield return new object[] { _testData.Stats.DefinitionIds.StatA, 0 };
            yield return new object[] { _testData.Stats.DefinitionIds.StatA, 100 };
        }

        [Theory,
         MemberData(nameof(GetNoStateTestData))]
        private void GetValue_NoState_ReturnsBaseValue(
            IIdentifier statDefinitionId,
            double baseValue)
        {
            // Setup
            var statManager = new StatManager(
                _fixture.EnchantmentCalculator,
                new MutableStatsProvider(new KeyValuePair<IIdentifier, double>(statDefinitionId, baseValue).Yield()),
                _fixture.ContextConverter);

            var statCalculationContext = new StatCalculationContext(
                new[] { new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider) },
                new IEnchantment[0]);

            // Execute
            var result = statManager.GetValue(
                statCalculationContext,
                statDefinitionId);

            // Assert
            Assert.Equal(baseValue, result);
        }
    }
}
