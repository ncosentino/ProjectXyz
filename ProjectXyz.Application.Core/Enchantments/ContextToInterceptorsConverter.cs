using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ContextToInterceptorsConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>>
    {
        private readonly IStateEnchantmentExpressionInterceptorFactory _stateEnchantmentExpressionInterceptorFactory;
        private readonly IInterval _unitInterval;

        public ContextToInterceptorsConverter(
            IStateEnchantmentExpressionInterceptorFactory stateEnchantmentExpressionInterceptorFactory,
            IInterval unitInterval)
        {
            _stateEnchantmentExpressionInterceptorFactory = stateEnchantmentExpressionInterceptorFactory;
            _unitInterval = unitInterval;
        }

        public IReadOnlyCollection<IEnchantmentExpressionInterceptor> Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var stateEnchantmentExpressionInterceptor = _stateEnchantmentExpressionInterceptorFactory.Create(
                enchantmentCalculatorContext.StateContextProvider,
                enchantmentCalculatorContext.Enchantments);

            // FIXME: this is narsty that this happens in here... dem "hardcoded" feelz.
            var termToValueMapping = new Dictionary<string, double>()
            {
                { "INTERVAL", enchantmentCalculatorContext.Elapsed.Divide(_unitInterval) }
            };
            var valueMappingExpressionEnchantmentExpressionInterceptor = new ValueMappingExpressionEnchantmentExpressionInterceptor(termToValueMapping);

            return new[]
            {
                stateEnchantmentExpressionInterceptor,
                valueMappingExpressionEnchantmentExpressionInterceptor,
            };
        }
    }
}