using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
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
                .OrderBy(x => x.Priority)
                .ToArray();
        }

        public void Register(IContextToExpressionInterceptorConverter converter)
        {
            _converters.Add(converter);
        }
    }
}