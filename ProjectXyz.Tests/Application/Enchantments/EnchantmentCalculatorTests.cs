using System;

using Xunit;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Tests.Application.Enchantments.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Application.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentCalculatorTests
    {
        [Fact]
        public void EnchantmentCalculator_CalculateSingleEnchantment_BoostsStat()
        {
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(10)
                .Build();

            var calculator = EnchantmentCalculator.Create();
            var stats = calculator.Calculate(
                StatCollection.Create(), 
                new IEnchantment[]
                {
                    enchantment,
                });

            Assert.Equal(1, stats.Count);
            Assert.True(
                stats.Contains(enchantment.StatId),
                "Expecting to contain " + enchantment.StatId + ".");
            Assert.Equal(enchantment.Value, stats[enchantment.StatId].Value);
        }

        [Fact]
        public void EnchantmentCalculator_CalculateMultipleEnchantmentsSameStat_BoostsStat()
        {
            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(10)
                .Build();

            var enchantments = new IEnchantment[]
            {
                enchantment,
                enchantment,
                enchantment
            };

            var calculator = EnchantmentCalculator.Create();
            var stats = calculator.Calculate(
                StatCollection.Create(),
                enchantments);

            Assert.Equal(1, stats.Count);
            Assert.True(
                stats.Contains(enchantment.StatId),
                "Expecting to contain " + enchantment.StatId + ".");
            Assert.Equal(enchantments.Length * enchantment.Value, stats[enchantment.StatId].Value);
        }

        [Fact]
        public void EnchantmentCalculator_CalculateBlessEnchantment_RemovesCurse()
        {
            var curseEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(-10)
                .WithStatusType(EnchantmentStatuses.Curse)
                .Build();
            
            var blessEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.Bless)
                .WithValue(0)
                .Build();

            var enchantments = new IEnchantment[]
            {
                curseEnchantment,
                blessEnchantment,
            };

            var calculator = EnchantmentCalculator.Create();
            var stats = calculator.Calculate(StatCollection.Create(), enchantments);

            Assert.False(
                stats.Contains(curseEnchantment.StatId),
                "Expecting the stat to be removed.");
        }

        [Fact]
        public void EnchantmentCalculator_CalculateCureEnchantment_RemovesDisease()
        {
            var diseaseEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(-10)
                .WithStatusType(EnchantmentStatuses.Disease)
                .Build();

            var cureEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.Cure)
                .WithValue(0)
                .Build();

            var enchantments = new IEnchantment[]
            {
                diseaseEnchantment,
                cureEnchantment,
            };

            var calculator = EnchantmentCalculator.Create();
            var stats = calculator.Calculate(StatCollection.Create(), enchantments);

            Assert.False(
                stats.Contains(diseaseEnchantment.StatId),
                "Expecting the stat to be removed.");
        }
    }
}