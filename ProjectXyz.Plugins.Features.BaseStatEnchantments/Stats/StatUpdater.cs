using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats
{
    public sealed class StatUpdater : IStatUpdater
    {
        private readonly IEnchantmentApplier _enchantmentApplier;
        private readonly IEnchantmentCalculatorContextFactory _enchantmentCalculatorContextFactory;

        public StatUpdater(
            IEnchantmentApplier enchantmentApplier,
            IEnchantmentCalculatorContextFactory enchantmentCalculatorContextFactory)
        {
            _enchantmentApplier = enchantmentApplier;
            _enchantmentCalculatorContextFactory = enchantmentCalculatorContextFactory;
        }

        public void Update(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments,
            Action<Action<IDictionary<IIdentifier, double>>> mutateStatsCallback,
            IInterval elapsed)
        {
            var enchantmentCalculatorContext = _enchantmentCalculatorContextFactory.CreateEnchantmentCalculatorContext(
                elapsed,
                enchantments);

            var updatedStats = _enchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            mutateStatsCallback(mutableStats =>
            {
                foreach (var statToValueMapping in updatedStats)
                {
                    mutableStats[statToValueMapping.Key] = statToValueMapping.Value;
                }
            });
        }
    }
}