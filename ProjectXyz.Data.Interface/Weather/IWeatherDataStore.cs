using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherDataStore
    {
        #region Properties
        IWeatherGroupingRepository WeatherGroupings { get; }
        #endregion
    }
}
