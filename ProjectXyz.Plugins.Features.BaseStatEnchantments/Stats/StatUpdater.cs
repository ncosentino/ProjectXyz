using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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

        public async Task UpdateAsync(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IReadOnlyCollection<IGameObject> enchantments,
            Func<Func<IDictionary<IIdentifier, double>, Task>, Task> mutateStatsCallback,
            double elapsedTurns)
        {
            var enchantmentCalculatorContext = _enchantmentCalculatorContextFactory.CreateEnchantmentCalculatorContext(
                elapsedTurns,
                enchantments);

            var updatedStats = _enchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            await mutateStatsCallback.Invoke(async mutableStats =>
            {
                foreach (var statToValueMapping in updatedStats)
                {
                    mutableStats[statToValueMapping.Key] = statToValueMapping.Value;
                }
            });
        }
    }
}