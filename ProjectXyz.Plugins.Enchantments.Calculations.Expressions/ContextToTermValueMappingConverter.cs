using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
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