using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Api
{
    public interface IEnchantmentCalculationPlugin : IPlugin
    {
        IContextToExpressionInterceptorConverter ContextToExpressionInterceptorConverter { get; }
    }
}