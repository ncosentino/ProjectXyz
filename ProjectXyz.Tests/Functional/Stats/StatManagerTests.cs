using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional.Stats
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
                new IComponent[] { },
                new IGameObject[0]);

            // Execute
            var result = statManager.GetValue(
                statCalculationContext,
                statDefinitionId);

            // Assert
            Assert.Equal(baseValue, result);
        }
    }
}
