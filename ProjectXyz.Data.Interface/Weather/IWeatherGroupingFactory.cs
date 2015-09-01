using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherGroupingFactory
    {
        #region Methods
        IWeatherGrouping Create(
            Guid id,
            Guid weatherId,
            Guid groupingId);
        #endregion
    }
}
