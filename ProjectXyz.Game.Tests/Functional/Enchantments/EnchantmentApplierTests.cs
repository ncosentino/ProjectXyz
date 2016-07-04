using System.Collections.Generic;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface.Collections;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentApplierTests
    {
        #region Constants
        private static readonly TestFixture FIXTURE = new TestFixture();
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, 5 };
            yield return new object[] { FIXTURE.Enchantments.Debuffs.StatA, -5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatB, 5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatC, 10 };
            yield return new object[] { FIXTURE.Enchantments.Debuffs.StatC, 5 };
            yield return new object[] { FIXTURE.Enchantments.PreNullifyStatA, -1 };
            yield return new object[] { FIXTURE.Enchantments.PostNullifyStatA, -1 };
            yield return new object[] { FIXTURE.Enchantments.RecursiveStatA, 0 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData("GetSingleEnchantmentNoBaseStatsTheoryData")]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStats_SingleStatExpectedValue(
            IEnchantment enchantment,
            double expectedValue)
        {
            var result = FIXTURE.EnchantmentApplier.ApplyEnchantments(
                StateContextProvider.Empty,
                StatCollection.Empty,
                enchantment.AsArray());

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId].Value);
        }
        #endregion
    }
}
