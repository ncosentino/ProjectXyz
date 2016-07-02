using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ExpressionEnchantmentExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> _statDefinitionToEnchantmentMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public ExpressionEnchantmentExpressionInterceptor(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> statDefinitionToEnchantmentMapping)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToEnchantmentMapping = statDefinitionToEnchantmentMapping;
        }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            var applicableEnchantments = _statDefinitionToEnchantmentMapping.GetValueOrDefault(
                statDefinitionId,
                () => new IExpressionEnchantment[0]);

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