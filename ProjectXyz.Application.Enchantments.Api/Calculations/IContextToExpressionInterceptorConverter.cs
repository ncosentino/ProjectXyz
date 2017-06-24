using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public interface IContextToExpressionInterceptorConverter : IConvert<IEnchantmentCalculatorContext, IEnchantmentExpressionInterceptor>
    {
    }
}