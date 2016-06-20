using System.Collections.Generic;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Weather
{
    public interface IWeatherManager
    {
        bool WeatherGroupingContainsWeatherDefinition(
            IIdentifier weatherGroupingId,
            IIdentifier weatherDefinitionid);
    }
}