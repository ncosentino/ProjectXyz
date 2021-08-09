using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IEnchantmentExpressionInterceptorConverter
    {
        IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);

        bool CanConvert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);
    }
}