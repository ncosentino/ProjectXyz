using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Stats.StatExpressions
{
    public sealed class EnchantmentExpressionInterceptorConverter : IEnchantmentExpressionInterceptorConverter
    {
        public IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor)
        {
            return new StatExpressionInterceptor(
                enchantmentExpressionInterceptor.Intercept,
                enchantmentExpressionInterceptor.Priority);
        }

        public bool CanConvert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor) => enchantmentExpressionInterceptor != null;
    }
}