using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherTableRepository
    {
        IEnumerable<IWeatherTable> GetWeatherTables(IFilterContext filterContext);
    }
}