using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IContextToExpressionInterceptorConverter : IConvert<IEnchantmentCalculatorContext, IEnchantmentExpressionInterceptor>
    {
    }
}