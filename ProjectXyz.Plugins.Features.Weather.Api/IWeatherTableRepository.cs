using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherTableRepository
    {
        IEnumerable<IWeatherTable> GetWeatherTables(IFilterContext filterContext);
    }
}