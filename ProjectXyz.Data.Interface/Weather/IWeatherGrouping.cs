using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Weather
{
    public interface IWeatherGrouping
    {
        #region Properties
        Guid Id { get; }

        Guid WeatherId { get; }

        Guid GroupingId { get; }
        #endregion
    }
}
