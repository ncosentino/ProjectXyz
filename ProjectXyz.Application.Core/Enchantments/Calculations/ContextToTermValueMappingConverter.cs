using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class ContextToTermValueMappingConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>>
    {
        private readonly IReadOnlyCollection<Func<IEnchantmentCalculatorContext, KeyValuePair<string, double>>> _valueMappers;

        public ContextToTermValueMappingConverter(IEnumerable<Func<IEnchantmentCalculatorContext, KeyValuePair<string, double>>> valueMappers)
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