using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
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