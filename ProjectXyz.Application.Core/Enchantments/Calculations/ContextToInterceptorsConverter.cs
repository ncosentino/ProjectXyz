using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class ContextToInterceptorsConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>>
    {
        private readonly IStateExpressionInterceptorFactory _stateExpressionInterceptorFactory;
        private readonly IValueMappingExpressionInterceptorFactory _valueMappingExpressionInterceptorFactory;
        private readonly IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>> _contextToTermValueMappingConverter;

        public ContextToInterceptorsConverter(
            IStateExpressionInterceptorFactory stateEnchantmentExpressionInterceptorFactory,
            IValueMappingExpressionInterceptorFactory valueMappingExpressionInterceptorFactory,
            IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>> contextToTermValueMappingConverter)
        {
            _stateExpressionInterceptorFactory = stateEnchantmentExpressionInterceptorFactory;
            _valueMappingExpressionInterceptorFactory = valueMappingExpressionInterceptorFactory;
            _contextToTermValueMappingConverter = contextToTermValueMappingConverter;
        }

        public IReadOnlyCollection<IEnchantmentExpressionInterceptor> Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var stateEnchantmentExpressionInterceptor = _stateExpressionInterceptorFactory.Create(
                enchantmentCalculatorContext.StateContextProvider,
                enchantmentCalculatorContext.Enchantments);

            var termToValueMapping = _contextToTermValueMappingConverter.Convert(enchantmentCalculatorContext);
            var valueMappingExpressionInterceptor = _valueMappingExpressionInterceptorFactory.Create(termToValueMapping);

            return new[]
            {
                stateEnchantmentExpressionInterceptor,
                valueMappingExpressionInterceptor,
            };
        }
    }
}