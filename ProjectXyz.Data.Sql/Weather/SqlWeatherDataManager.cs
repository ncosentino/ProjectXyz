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
        #endregion

        #region Constructors
        private SqlWeatherDataManager(IDatabase database)
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
        public static IWeatherDataManager Create(IDatabase database)
        {
            var dataManager = new SqlWeatherDataManager(database);
            return dataManager;
        }
        #endregion
    }
}
