using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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

        public IEnumerable<IGameObject> GetEnchantments(
            IGameObject gameObject,
            Predicate<IGameObject> visited,
            IIdentifier target,
            IIdentifier statId,
            IStatCalculationContext context)
        {
            if (visited(gameObject))
            {
                return new IGameObject[0];
            }

            var results = new ConcurrentBag<IGameObject>();
            Parallel.ForEach(_getEnchantmentsMapping, entry =>
            {
                if (!entry.CanGetEnchantments(gameObject))
                {
                    return;
                }

                foreach(var enchantment in entry.GetEnchantments(
                    this,
                    this,
                    gameObject,
                    target,
                    statId,
                    context))
                {
                    results.Add(enchantment);
                }
            });

            return results;
        }

        public double GetStatValue(
            IGameObject gameObject,
            IIdentifier statId,
            IStatCalculationContext context)
        {
            var match = _getStatsMapping.FirstOrDefault(x => x.CanCalculateStat(gameObject));
            if (match == null)
            {
                return 0;
            }

            var enchantments = GetEnchantments(
                gameObject,
                _ => false,
                new StringIdentifier("self"),
                statId,
                context)
                .ToArray();
            var statValue = match.CalculateStat(
                gameObject,
                enchantments,
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
