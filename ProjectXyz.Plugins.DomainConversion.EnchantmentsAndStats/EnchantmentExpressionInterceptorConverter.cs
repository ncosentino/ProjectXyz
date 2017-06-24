using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndStats
{
    public sealed class EnchantmentExpressionInterceptorConverter : IEnchantmentExpressionInterceptorConverter
    {
        public IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor)
        {
            return new StatExpressionInterceptor(enchantmentExpressionInterceptor.Intercept, enchantmentExpressionInterceptor.Priority);
        }

        public bool CanConvert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor) => enchantmentExpressionInterceptor != null;
    }
}