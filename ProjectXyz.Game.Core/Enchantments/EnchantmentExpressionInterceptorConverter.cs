using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Core.Enchantments
{
    public sealed class EnchantmentExpressionInterceptorConverter : IEnchantmentExpressionInterceptorConverter
    {
        public IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor)
        {
            return new StatExpressionInterceptor(enchantmentExpressionInterceptor.Intercept);
        }
    }
}