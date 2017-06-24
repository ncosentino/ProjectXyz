using System.Collections.Generic;
using ProjectXyz.Application.Core.Stats;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Tests.Functional.TestingData;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Stats
{
    public sealed class StatManagerTests
    {
        #region Constants
        private static readonly TestData TEST_DATA = new TestData();
        #endregion

        #region Fields
        private readonly TestFixture _fixture;
        #endregion

        #region Constructors
        public StatManagerTests()
        {
            _fixture = new TestFixture(TEST_DATA);
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetNoStateTestData()
        {
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, -100 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, 0 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, 100 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetNoStateTestData))]
        private void GetValue_NoState_ReturnsBaseValue(
            IIdentifier statDefinitionId,
            double baseValue)
        {
            // Setup
            var statManager = new StatManager(
                _fixture.EnchantmentCalculator,
                new ImmutableStatsProvider(new KeyValuePair<IIdentifier, double>(statDefinitionId, baseValue).Yield()), 
                new ContextConverter(TEST_DATA.ZeroInterval));

            var statCalculationContext = new StatCalculationContext(
                new[] { TEST_DATA.StatesPlugin.StateContextProvider },
                new IEnchantment[0]);

            // Execute
            var result = statManager.GetValue(
                statCalculationContext,
                statDefinitionId);

            // Assert
            Assert.Equal(baseValue, result);
        }
        #endregion
    }
}
