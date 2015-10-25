using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherRepository
    {
        #region Methods
        IWeather Add(
            Guid id,
            Guid nameStringResourceId);

        IWeather GetById(Guid id);

        IEnumerable<IWeather> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
