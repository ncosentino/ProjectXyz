using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Weather
{
    public interface IWeather
    {
        IIdentifier WeatherDefinitionId { get; }
    }
}
