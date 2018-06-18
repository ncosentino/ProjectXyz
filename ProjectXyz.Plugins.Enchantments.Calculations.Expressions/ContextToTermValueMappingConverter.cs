using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public sealed class ContextToTermValueMappingConverter : IContextToTermValueMappingConverter
    {
        private readonly IReadOnlyCollection<ValueMapperDelegate> _valueMappers;

        public ContextToTermValueMappingConverter(IEnumerable<ValueMapperDelegate> valueMappers)
        {
            _valueMappers = valueMappers.ToArray();
        }

        public IReadOnlyDictionary<string, double> Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var termToValueMapping = _valueMappers
                .Select(x => x(enchantmentCalculatorContext))
                .ToDictionary();
            return termToValueMapping;
        }
    }
}