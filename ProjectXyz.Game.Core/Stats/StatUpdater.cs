using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatUpdater : IStatUpdater
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IEnchantmentApplier _enchantmentApplier;
        private readonly IEnchantmentProvider _enchantmentProvider;
        private readonly IMutableStatsProvider _mutableStatsProvider;

        public StatUpdater(
            IStateContextProvider stateContextProvider,
            IMutableStatsProvider mutableStatsProvider,
            IEnchantmentProvider enchantmentProvider,
            IEnchantmentApplier enchantmentApplier)
        {
            _stateContextProvider = stateContextProvider;
            _mutableStatsProvider = mutableStatsProvider;
            _enchantmentProvider = enchantmentProvider;
            _enchantmentApplier = enchantmentApplier;
        }

        public void Update(
            IElapsedTimeTriggerMechanic elapsedTimeTriggerMechanic,
            IElapsedTimeTriggerComponent elapsedTimeTriggerComponent)
        {
            var enchantmentCalculatorContext = new EnchantmentCalculatorContext(
                _stateContextProvider,
                elapsedTimeTriggerComponent.Elapsed,
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