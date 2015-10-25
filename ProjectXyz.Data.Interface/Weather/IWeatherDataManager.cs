using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherDataManager
    {
        #region Properties
        IWeatherGroupingRepository WeatherGroupings { get; }

        IWeatherRepository Weather { get; }
        #endregion
    }
}
