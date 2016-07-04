using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentApplier : IEnchantmentApplier
    {
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IStatFactory _statFactory;

        public EnchantmentApplier(
            IStatFactory statFactory,
            IEnchantmentCalculator enchantmentCalculator)
        {
            _statFactory = statFactory;
            _enchantmentCalculator = enchantmentCalculator;
        }

        public IReadOnlyDictionary<IIdentifier, IStat> ApplyEnchantments(
            IStateContextProvider stateContextProvider,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            var newStats = baseStats.ToDictionary();

            foreach (var enchantment in enchantments)
            {
                var statDefinitionId = enchantment.StatDefinitionId;
                var value = _enchantmentCalculator.Calculate(
                    stateContextProvider,
                    newStats,
                    enchantment.AsArray(),
                    statDefinitionId);
                newStats[statDefinitionId] = _statFactory.Create(
                    statDefinitionId,
                    value);
            }

            return newStats;
        }
    }
}