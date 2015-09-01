using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Weather;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Sql.Weather
{
    public sealed class SqlWeatherDataStore : IWeatherDataStore
    {
        #region Fields
        private readonly IWeatherGroupingRepository _weatherGroupingRepository;
        #endregion

        #region Constructors
        private SqlWeatherDataStore(IDatabase database)
        {
            var weatherGroupingFactory = WeatherGroupingFactory.Create();
            _weatherGroupingRepository = WeatherTypeGroupingRepository.Create(
                database,
                weatherGroupingFactory);
        }
        #endregion

        #region Properties
        public IWeatherGroupingRepository WeatherGroupings { get { return _weatherGroupingRepository; } }
        #endregion

        #region Methods
        public static IWeatherDataStore Create(IDatabase database)
        {
            var dataStore = new SqlWeatherDataStore(database);
            return dataStore;
        }
        #endregion
    }
}
