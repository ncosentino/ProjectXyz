using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Core.Weather
{
    public sealed class WeatherGrouping : IWeatherGrouping
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _weatherId;
        private readonly Guid _groupingId;
        #endregion

        #region Constructors
        private WeatherGrouping(
            Guid id,
            Guid weatherId,
            Guid groupingId)
        {
            _id = id;
            _weatherId = weatherId;
            _groupingId = groupingId;
        }
        #endregion

        #region Properties
        public Guid Id { get { return _id; } }

        public Guid WeatherId { get { return _weatherId; } }

        public Guid GroupingId { get { return _groupingId; } }
        #endregion

        #region Methods
        public static IWeatherGrouping Create(
            Guid id,
            Guid weatherId,
            Guid groupingId)
        {
            var weatherGrouping = new WeatherGrouping(
                id,
                weatherId,
                groupingId);
            return weatherGrouping;
        }
        #endregion
    }
}
