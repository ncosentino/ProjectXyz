using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class ContextToExpressionInterceptorConverter : IContextToExpressionInterceptorConverter
    {
        private readonly IStateExpressionInterceptorFactory _stateExpressionInterceptorFactory;

        public ContextToExpressionInterceptorConverter(IStateExpressionInterceptorFactory stateExpressionInterceptorFactory)
        {
            _stateExpressionInterceptorFactory = stateExpressionInterceptorFactory;
        }

        public IEnchantmentExpressionInterceptor Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var stateEnchantmentExpressionInterceptor = _stateExpressionInterceptorFactory.Create(enchantmentCalculatorContext.Enchantments);
            return stateEnchantmentExpressionInterceptor;
        }
    }
}
