using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public sealed class ContextToExpressionInterceptorConverter : IContextToExpressionInterceptorConverter
    {
        private readonly IStatToTermExpressionInterceptorFactory _statToTermExpressionInterceptorFactory;

        public ContextToExpressionInterceptorConverter(IStatToTermExpressionInterceptorFactory statToTermExpressionInterceptorFactory)
        {
            _statToTermExpressionInterceptorFactory = statToTermExpressionInterceptorFactory;
        }

        public IEnchantmentExpressionInterceptor Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var stateEnchantmentExpressionInterceptor = _statToTermExpressionInterceptorFactory.Create(enchantmentCalculatorContext.Enchantments);
            return stateEnchantmentExpressionInterceptor;
        }
    }
}