using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherTableRepository
    {
        IEnumerable<IWeatherTable> GetWeatherTables(IFilterContext filterContext);
    }
}