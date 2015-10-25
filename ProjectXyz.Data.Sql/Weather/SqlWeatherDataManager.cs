using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Weather;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Sql.Weather
{
    public sealed class SqlWeatherDataManager : IWeatherDataManager
    {
        #region Fields
        private readonly IWeatherGroupingRepository _weatherGroupingRepository;
        private readonly IWeatherRepository _weatherRepository;
        #endregion

        #region Constructors
        private SqlWeatherDataManager(IDatabase database)
        {
            var weatherGroupingFactory = WeatherGroupingFactory.Create();
            _weatherGroupingRepository = WeatherTypeGroupingRepository.Create(
                database,
                weatherGroupingFactory);
            var weatherFactory = WeatherFactory.Create();
            _weatherRepository = WeatherRepository.Create(
                database,
                weatherFactory);
        }
        #endregion

        #region Properties
        public IWeatherGroupingRepository WeatherGroupings { get { return _weatherGroupingRepository; } }

        public IWeatherRepository Weather { get { return _weatherRepository; } }
        #endregion

        #region Methods
        public static IWeatherDataManager Create(IDatabase database)
        {
            var dataManager = new SqlWeatherDataManager(database);
            return dataManager;
        }
        #endregion
    }
}
