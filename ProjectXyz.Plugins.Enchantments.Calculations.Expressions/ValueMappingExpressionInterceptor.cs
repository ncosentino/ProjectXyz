using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public sealed class ValueMappingExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyDictionary<string, double> _termToValueMapping;

        public ValueMappingExpressionInterceptor(
            IReadOnlyDictionary<string, double> termToValueMapping,
            int priority)
        {
            _termToValueMapping = termToValueMapping;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            expression = _termToValueMapping
                .Aggregate(
                    expression,
                    (current, termToValue) => current.Replace(
                        termToValue.Key,
                        $"({termToValue.Value})"));
            return expression;
        }
    }
}