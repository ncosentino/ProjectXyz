using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatUpdater : IStatUpdater
    {
        private readonly IEnchantmentApplier _enchantmentApplier;
        private readonly IEnchantmentProvider _enchantmentProvider;
        private readonly IMutableStatsProvider _mutableStatsProvider;
        private readonly IEnchantmentCalculatorContextFactory _enchantmentCalculatorContextFactory;

        public StatUpdater(
            IMutableStatsProvider mutableStatsProvider,
            IEnchantmentProvider enchantmentProvider,
            IEnchantmentApplier enchantmentApplier,
            IEnchantmentCalculatorContextFactory enchantmentCalculatorContextFactory)
        {
            _mutableStatsProvider = mutableStatsProvider;
            _enchantmentProvider = enchantmentProvider;
            _enchantmentApplier = enchantmentApplier;
            _enchantmentCalculatorContextFactory = enchantmentCalculatorContextFactory;
        }

        public void Update(IInterval elapsed)
        {
            var enchantmentCalculatorContext = _enchantmentCalculatorContextFactory.CreateEnchantmentCalculatorContext(
                elapsed,
                _enchantmentProvider.Enchantments);

            var updatedStats = _enchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                _mutableStatsProvider.Stats);

            _mutableStatsProvider.UsingMutableStats(mutableStats =>
            {
                foreach (var statToValueMapping in updatedStats)
                {
                    mutableStats[statToValueMapping.Key] = statToValueMapping.Value;
                }
            });
        }
    }
}