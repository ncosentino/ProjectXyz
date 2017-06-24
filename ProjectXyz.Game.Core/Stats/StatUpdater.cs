using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Enchantments.Interface;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatUpdater : IStatUpdater
    {
        private readonly IComponentCollection _components;
        private readonly IEnchantmentApplier _enchantmentApplier;
        private readonly IEnchantmentProvider _enchantmentProvider;
        private readonly IMutableStatsProvider _mutableStatsProvider;

        public StatUpdater(
            IEnumerable<IComponent> components,
            IMutableStatsProvider mutableStatsProvider,
            IEnchantmentProvider enchantmentProvider,
            IEnchantmentApplier enchantmentApplier)
        {
            _components = new ComponentCollection(components);
            _mutableStatsProvider = mutableStatsProvider;
            _enchantmentProvider = enchantmentProvider;
            _enchantmentApplier = enchantmentApplier;
        }

        public void Update(
            IElapsedTimeTriggerMechanic elapsedTimeTriggerMechanic,
            IElapsedTimeTriggerComponent elapsedTimeTriggerComponent)
        {
            var enchantmentCalculatorContext = new EnchantmentCalculatorContext(
                _components,
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