using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
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