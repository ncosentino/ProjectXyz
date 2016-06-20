using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Application.Interface.Weather;
using ClassLibrary1.Plugins.Api.Enchantments;

namespace ClassLibrary1.Plugins.Core.Enchantments
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
