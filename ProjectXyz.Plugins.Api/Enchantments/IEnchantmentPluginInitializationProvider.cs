using ProjectXyz.Application.Interface.Weather;

namespace ProjectXyz.Plugins.Api.Enchantments
{
    public interface IEnchantmentPluginInitializationProvider
    {
        IWeatherManager WeatherManager { get; }
    }
}
