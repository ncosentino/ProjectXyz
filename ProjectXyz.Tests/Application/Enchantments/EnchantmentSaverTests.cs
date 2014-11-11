﻿using System;

using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Tests.Application.Enchantments.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Application.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentSaverTests
    {
        [Fact]
        public void SaveMetadata()
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