using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IContextToExpressionInterceptorConverter : IConvert<IEnchantmentCalculatorContext, IEnchantmentExpressionInterceptor>
    {
    }
}