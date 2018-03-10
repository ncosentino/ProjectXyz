using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public sealed class StatToTermExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IEnchantmentExpressionComponent>> _statDefinitionToComponentMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public StatToTermExpressionInterceptor(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IEnchantmentExpressionComponent>> statDefinitionToComponentMapping,
            int priority)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToComponentMapping = statDefinitionToComponentMapping;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            var applicableEnchantments = _statDefinitionToComponentMapping.GetValueOrDefault(
                statDefinitionId,
                () => new IEnchantmentExpressionComponent[0]);

            var term = _statDefinitionIdToTermMapping[statDefinitionId];

            expression = applicableEnchantments.Aggregate(
                expression,
                (current, enchantment) => enchantment.Expression.Replace(
                    term,
                    $"({current})"));
            return expression;
        }
    }
}
