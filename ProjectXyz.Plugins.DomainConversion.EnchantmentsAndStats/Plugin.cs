using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats.Plugins;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndStats
{
    public sealed class Plugin : IEnchantmentsAndStatsPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            EnchantmentExpressionInterceptorConverters = new[]
            {
                new EnchantmentExpressionInterceptorConverter(),
            };
        }

        public IReadOnlyCollection<IEnchantmentExpressionInterceptorConverter> EnchantmentExpressionInterceptorConverters { get; }
    }
}
