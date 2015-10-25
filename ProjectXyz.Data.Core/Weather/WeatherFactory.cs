using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Core.Weather
{
    public sealed class WeatherFactory : IWeatherFactory
    {
        #region Constructors
        private WeatherFactory()
        {
        }
        #endregion

        #region Methods
        public static IWeatherFactory Create()
        {
            var factory = new WeatherFactory();
            return factory;
        }

        public IWeather Create(
            Guid id,
            Guid nameStringResourceId)
        {
            var weather = Weather.Create(
                id,
                nameStringResourceId);
            return weather;
        }
        #endregion
    }
}
