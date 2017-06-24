using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Api.DomainConversions.EnchantmentsAndStats
{
    public interface IEnchantmentExpressionInterceptorConverter
    {
        IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);

        bool CanConvert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);
    }
}