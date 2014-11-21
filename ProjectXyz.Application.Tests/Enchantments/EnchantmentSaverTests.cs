using System;

using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Application.Tests.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentSaverTests
    {
        [Fact]
        public void EnchantmentSaver_SaveEnchantment_GeneratesValidData()
        {
            var enchantment = new MockEnchantmentBuilder()
                .WithStatId(ActorStats.MaximumLife)
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithRemainingTime(TimeSpan.FromSeconds(123))
                .WithValue(1234567)
                .Build();

            var enchantmentSaver = EnchantmentSaver.Create();
            var savedData = enchantmentSaver.Save(enchantment);

            Assert.Equal(enchantment.CalculationId, savedData.CalculationId);
            Assert.Equal(enchantment.RemainingDuration, savedData.RemainingDuration);
            Assert.Equal(enchantment.StatId, savedData.StatId);
            Assert.Equal(enchantment.Value, savedData.Value);
        }
    }
}