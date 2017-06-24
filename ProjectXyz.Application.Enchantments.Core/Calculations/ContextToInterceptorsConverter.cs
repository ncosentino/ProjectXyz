using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Core.Calculations
{
    public sealed class ContextToInterceptorsConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>>
    {
        private readonly List<IContextToExpressionInterceptorConverter> _converters;

        public ContextToInterceptorsConverter()
        {
            _converters = new List<IContextToExpressionInterceptorConverter>();
        }

        public IReadOnlyCollection<IEnchantmentExpressionInterceptor> Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            return _converters
                .Select(x => x.Convert(enchantmentCalculatorContext))
                .ToArray();
        }

        public void Register(IContextToExpressionInterceptorConverter converter)
        {
            _converters.Add(converter);
        }
    }
}