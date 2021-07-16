using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Testing;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Enchantments
{
    public sealed class ActiveEnchantmentManagerTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;
        private static readonly ITurnInfoFactory _turnInfoFactory;
        private static readonly IActionInfoFactory _actionInfoFactory;
        private static readonly IElapsedTurnsTriggerSourceMechanic _elapsedTurnsTriggerSourceMechanic;
        private static readonly IElapsedActionsTriggerSourceMechanic _elapsedActionsTriggerSourceMechanic;

        static ActiveEnchantmentManagerTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
            _turnInfoFactory = _fixture.LifeTimeScope.Resolve<ITurnInfoFactory>();
            _actionInfoFactory = _fixture.LifeTimeScope.Resolve<IActionInfoFactory>();
            _elapsedTurnsTriggerSourceMechanic = _fixture.LifeTimeScope.Resolve<IElapsedTurnsTriggerSourceMechanic>();
            _elapsedActionsTriggerSourceMechanic = _fixture.LifeTimeScope.Resolve<IElapsedActionsTriggerSourceMechanic>();
        }

        public static IEnumerable<object[]> GetElapsedTurnsTheoryData()
        {
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, true };
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 10, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 11, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 11, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 11, true };
        }

        public static IEnumerable<object[]> GetElapsedActionsTheoryData()
        {
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, true };
            yield return new object[] { _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 10, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 11, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 10, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Actions, 11, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 0, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 5, true };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 10, false };
            yield return new object[] { _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10ActionsIntervalIgnorant, 11, false };
        }

        [Theory,
         MemberData(nameof(GetElapsedTurnsTheoryData))]
        private async Task ElapseTurns_SingleEnchantment_ContainsExpectedValue(
            IGameObject enchantment,
            double elapsedTurns,
            bool contains)
        {
            var activeEnchantmentManager = _fixture
                .ActiveEnchantmentManagerFactory
                .Create();
            activeEnchantmentManager.Add(enchantment);
            Ensures.Equal(
                true,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                "Expecting the enchantment to be contained before elapsing any time.");
            if (elapsedTurns > 0)
            {
                for (var i = 0; i < elapsedTurns; i++)
                {
                    var turnInfo = _turnInfoFactory.Create(
                        null,
                        new FrozenList<IGameObject>(new IGameObject[] { }),
                        1);
                    await _elapsedTurnsTriggerSourceMechanic.UpdateAsync(turnInfo);
                }
            }
            else
            {
                var turnInfo = _turnInfoFactory.Create(
                    null,
                    new FrozenList<IGameObject>(new IGameObject[] { }),
                    0);
                await _elapsedTurnsTriggerSourceMechanic.UpdateAsync(turnInfo);
            }

            AssertEx.Equal(
                contains,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }

        [Theory,
         MemberData(nameof(GetElapsedActionsTheoryData))]
        private async Task ElapseActions_SingleEnchantment_ContainsExpectedValue(
            IGameObject enchantment,
            double elapsedActions,
            bool contains)
        {
            var activeEnchantmentManager = _fixture
                .ActiveEnchantmentManagerFactory
                .Create();
            activeEnchantmentManager.Add(enchantment);
            Ensures.Equal(
                true,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                "Expecting the enchantment to be contained before elapsing any time.");
            if (elapsedActions > 0)
            {
                for (var i = 0; i < elapsedActions; i++)
                {
                    var actionInfo = _actionInfoFactory.Create(
                        null,
                        new FrozenList<IGameObject>(new IGameObject[] { }),
                        1);
                    await _elapsedActionsTriggerSourceMechanic.UpdateAsync(actionInfo);
                }
            }
            else
            {
                var actionInfo = _actionInfoFactory.Create(
                    null,
                    new FrozenList<IGameObject>(new IGameObject[] { }),
                    0);
                await _elapsedActionsTriggerSourceMechanic.UpdateAsync(actionInfo);
            }

            AssertEx.Equal(
                contains,
                activeEnchantmentManager.Enchantments.Contains(enchantment),
                $"{(contains ? "Expecting" : "Not expecting")} the enchantment to be contained.");
        }
    }
}
