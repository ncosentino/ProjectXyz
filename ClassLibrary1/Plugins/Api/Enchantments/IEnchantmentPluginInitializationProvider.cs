using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Application.Interface.Weather;

namespace ClassLibrary1.Plugins.Api.Enchantments
{
    public interface IEnchantmentPluginInitializationProvider
    {
        IWeatherManager WeatherManager { get; }
    }
}
