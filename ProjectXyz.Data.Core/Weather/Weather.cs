using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Core.Weather
{
    public sealed class Weather : IWeather
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        #endregion

        #region Constructors
        private Weather(
            Guid id,
            Guid nameStringResourceId)
        {
            _id = id;
            _nameStringResourceId = nameStringResourceId;
        }
        #endregion

        #region Properties
        public Guid Id { get { return _id; } }

        public Guid NameStringResourceId { get { return _nameStringResourceId; } }
        #endregion

        #region Methods
        public static IWeather Create(
            Guid id,
            Guid nameStringResourceId)
        {
            var weather = new Weather(
                id,
                nameStringResourceId);
            return weather;
        }
        #endregion
    }
}
