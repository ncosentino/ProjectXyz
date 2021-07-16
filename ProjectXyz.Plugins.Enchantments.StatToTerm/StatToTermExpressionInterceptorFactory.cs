using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public sealed class StatToTermExpressionInterceptorFactory : IStatToTermExpressionInterceptorFactory
    {
        private readonly int _priority;
        private readonly IStatDefinitionToTermConverter _statDefinitionIdToTermMapping;

        public StatToTermExpressionInterceptorFactory(
            IStatDefinitionToTermConverter statDefinitionIdToTermMapping,
            int priority)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _priority = priority;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IGameObject> enchantments)
        {
            var statDefinitionToComponentMapping = enchantments
                .GroupBy(
                    enchantment => enchantment.TryGetFirst<IHasStatDefinitionIdBehavior>(out var statDefinitionIdBehavior)
                        ? statDefinitionIdBehavior.StatDefinitionId
                        : null,
                    enchantment => enchantment)
                .Where(x => x != null)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .SelectMany(enchantment => enchantment.Get<IEnchantmentExpressionBehavior>())
                        .OrderBy(component => component.CalculationPriority)
                        .ToReadOnlyCollection());

            var interceptor = new StatToTermExpressionInterceptor(
                _statDefinitionIdToTermMapping,
                statDefinitionToComponentMapping,
                _priority);
            return interceptor;
        }
    }
}