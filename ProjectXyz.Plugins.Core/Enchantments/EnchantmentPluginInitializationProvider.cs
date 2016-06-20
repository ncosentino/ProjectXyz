using ProjectXyz.Application.Interface.Weather;
using ProjectXyz.Plugins.Api.Enchantments;

namespace ProjectXyz.Plugins.Core.Enchantments
{
    public sealed class EnchantmentPluginInitializationProvider : IEnchantmentPluginInitializationProvider
    {
        public EnchantmentPluginInitializationProvider(IWeatherManager weatherManager)
        {
            WeatherManager = weatherManager;
        }

        public IWeatherManager WeatherManager { get; }
    }
}
