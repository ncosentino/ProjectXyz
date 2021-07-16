using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Api;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments
{
    public sealed class EnchantmentApplier : IEnchantmentApplier
    {
        private readonly IEnchantmentCalculator _enchantmentCalculator;

        public EnchantmentApplier(IEnchantmentCalculator enchantmentCalculator)
        {
            _enchantmentCalculator = enchantmentCalculator;
        }

        public IReadOnlyDictionary<IIdentifier, double> ApplyEnchantments(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats)
        {
            var newStats = baseStats.ToDictionary();

            foreach (var enchantment in enchantmentCalculatorContext
                .Enchantments
                .Where(x => x.Has<IAppliesToBaseStat>()))
            {
                if (!enchantment.TryGetFirst<IHasStatDefinitionIdBehavior>(out var statDefinitionIdBehavior))
                {
                    continue;
                }

                var statDefinitionId = statDefinitionIdBehavior.StatDefinitionId;
                var value = _enchantmentCalculator.Calculate(
                    enchantmentCalculatorContext,
                    newStats,
                    statDefinitionId);
                newStats[statDefinitionId] = value;
            }

            return newStats;
        }
    }
}