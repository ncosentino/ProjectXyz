﻿using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
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
        #endregion
    }
}
