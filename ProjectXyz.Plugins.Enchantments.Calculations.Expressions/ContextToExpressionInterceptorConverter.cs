using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public sealed class ContextToExpressionInterceptorConverter : IContextToExpressionInterceptorConverter
    {
        private readonly IContextToTermValueMappingConverter _contextToTermValueMappingConverter;
        private readonly IValueMappingExpressionInterceptorFactory _valueMappingExpressionInterceptorFactory;

        public ContextToExpressionInterceptorConverter(
            IContextToTermValueMappingConverter contextToTermValueMappingConverter,
            IValueMappingExpressionInterceptorFactory valueMappingExpressionInterceptorFactory)
        {
            _contextToTermValueMappingConverter = contextToTermValueMappingConverter;
            _valueMappingExpressionInterceptorFactory = valueMappingExpressionInterceptorFactory;
        }

        public IEnchantmentExpressionInterceptor Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var termToValueMapping = _contextToTermValueMappingConverter.Convert(enchantmentCalculatorContext);
            var valueMappingExpressionInterceptor = _valueMappingExpressionInterceptorFactory.Create(termToValueMapping);
            return valueMappingExpressionInterceptor;
        }
    }
}
