using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Core.Weather
{
    public sealed class WeatherGroupingFactory : IWeatherGroupingFactory
    {
        #region Constructors
        private WeatherGroupingFactory()
        {
        }
        #endregion

        #region Methods
        public static IWeatherGroupingFactory Create()
        {
            var factory = new WeatherGroupingFactory();
            return factory;
        }

        public IWeatherGrouping Create(
            Guid id,
            Guid weatherId,
            Guid groupingId)
        {
            var weatherGrouping = WeatherGrouping.Create(
                id,
                weatherId,
                groupingId);
            return weatherGrouping;
        }
        #endregion
    }
}
