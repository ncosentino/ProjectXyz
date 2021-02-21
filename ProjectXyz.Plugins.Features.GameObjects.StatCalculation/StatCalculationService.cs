using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation
{
    public sealed class StatCalculationService : IStatCalculationService
    {
        private readonly List<IStatCalculatorHandler> _getStatsMapping;
        private readonly List<IGetEnchantmentsHandler> _getEnchantmentsMapping;

        public StatCalculationService(
            IEnumerable<IDiscoverableGetEnchantmentsHandler> getEnchantmentsHandlers,
            IEnumerable<IDiscoverableStatCalculatorHandler> statCalculatorHandlers)
        {
            _getEnchantmentsMapping = new List<IGetEnchantmentsHandler>(getEnchantmentsHandlers);
            _getStatsMapping = new List<IStatCalculatorHandler>(statCalculatorHandlers);
        }

        public IEnumerable<IEnchantment> GetEnchantments(
            IHasBehaviors gameObject,
            Predicate<IHasBehaviors> visited,
            IIdentifier target,
            IIdentifier statId,
            IStatCalculationContext context)
        {
            if (visited(gameObject))
            {
                yield break;
            }

            foreach (var entry in _getEnchantmentsMapping)
            {
                if (!entry.CanGetEnchantments(gameObject))
                {
                    continue;
                }

                foreach (var enchantment in entry.GetEnchantments(
                    this,
                    this,
                    gameObject,
                    target,
                    statId,
                    context))
                {
                    yield return enchantment;
                }
            }
        }

        public double GetStatValue(
            IHasBehaviors gameObject,
            IIdentifier statId,
            IStatCalculationContext context)
        {
            var match = _getStatsMapping.FirstOrDefault(x => x.CanCalculateStat(gameObject));
            if (match == null)
            {
                return 0;
            }

            var statValue = match.CalculateStat(
                gameObject,
                GetEnchantments(
                    gameObject,
                    _ => false,
                    new StringIdentifier("self"),
                    statId,
                    context)
                    .ToArray(),
                statId,
                context);
            return statValue;
        }

        public void Register(IGetEnchantmentsHandler handler) =>
            _getEnchantmentsMapping.Add(handler);

        public void Register(IStatCalculatorHandler handler) =>
            _getStatsMapping.Add(handler);
    }
}
