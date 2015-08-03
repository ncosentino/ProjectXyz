using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Enchantments
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

            var calculator = EnchantmentCalculator.Create(StatFactory.Create(), new Mock<IStatusNegationRepository>().Object);
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

            var calculator = EnchantmentCalculator.Create(StatFactory.Create(), new Mock<IStatusNegationRepository>().Object);
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
        public void EnchantmentCalculator_CalculateStatusNegation_NegatesExpectedStatus()
        {
            // Setup
            var status = EnchantmentStatuses.Curse;
            var stat = ActorStats.Bless;

            var curseEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(-10)
                .WithStatusType(status)
                .Build();
            
            var blessEnchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(stat)
                .WithValue(0)
                .Build();

            var enchantments = new IEnchantment[]
            {
                curseEnchantment,
                blessEnchantment,
            };

            var statusNegationRepository = new Mock<IStatusNegationRepository>();
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(new[]
                {
                    StatusNegation.Create(stat, status)
                });

            var calculator = EnchantmentCalculator.Create(StatFactory.Create(), statusNegationRepository.Object);

            // Execute
            var stats = calculator.Calculate(StatCollection.Create(), enchantments);

            // Assert
            Assert.False(
                stats.Contains(curseEnchantment.StatId),
                "Expecting the stat to be removed.");
        }
    }
}