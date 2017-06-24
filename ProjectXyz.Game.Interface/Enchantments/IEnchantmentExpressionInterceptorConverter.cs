using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Game.Interface.Enchantments
{
    public interface IEnchantmentExpressionInterceptorConverter
    {
        IStatExpressionInterceptor Convert(IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor);
    }
}