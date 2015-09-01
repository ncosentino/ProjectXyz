using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherGroupingRepository
    {
        #region Methods
        IWeatherGrouping Add(
            Guid id,
            Guid weatherId,
            Guid groupingId);

        IWeatherGrouping GetById(Guid id);

        IEnumerable<IWeatherGrouping> GetByGroupingId(Guid groupingId);

        IEnumerable<IWeatherGrouping> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
