using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public sealed class StatToTermExpressionInterceptorFactory : IStatToTermExpressionInterceptorFactory
    {
        private readonly int _priority;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public StatToTermExpressionInterceptorFactory(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            int priority)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _priority = priority;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IEnchantment> enchantments)
        {
            var statDefinitionToComponentMapping = enchantments
                .GroupBy(
                    enchantment => enchantment.StatDefinitionId,
                    enchantment => enchantment)
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