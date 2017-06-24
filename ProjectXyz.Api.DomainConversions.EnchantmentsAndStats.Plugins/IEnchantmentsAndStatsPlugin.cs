using System.Collections.Generic;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Api.DomainConversions.EnchantmentsAndStats.Plugins
{
    public interface IEnchantmentsAndStatsPlugin : IPlugin
    {
        IReadOnlyCollection<IEnchantmentExpressionInterceptorConverter> EnchantmentExpressionInterceptorConverters { get; }
    }
}
