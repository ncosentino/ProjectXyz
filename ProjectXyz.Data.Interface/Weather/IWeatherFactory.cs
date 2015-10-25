using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherFactory
    {
        #region Methods
        IWeather Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
