using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.Application.Interface.Weather;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Core.Weather
{
    public sealed class WeatherManager : IWeatherManager
    {
        public bool WeatherGroupingContainsWeatherDefinition(
            IIdentifier weatherGroupingId,
            IIdentifier weatherDefinitionId)
        {
            return true;
        }
    }
}
