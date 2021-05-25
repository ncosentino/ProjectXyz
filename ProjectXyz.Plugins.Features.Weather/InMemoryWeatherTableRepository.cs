using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class InMemoryWeatherTableRepository : IDiscoverableWeatherTableRepository
    {
        private readonly IReadOnlyCollection<IWeatherTable> _weatherTables;
        private readonly IAttributeFilterer _attributeFilterer;

        public InMemoryWeatherTableRepository(
            IEnumerable<IWeatherTable> weatherTables,
            IAttributeFilterer attributeFilterer)
        {
            _weatherTables = weatherTables.ToArray();
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IWeatherTable> GetWeatherTables(IFilterContext filterContext)
        {
            var matches = _attributeFilterer.Filter(
                _weatherTables,
                filterContext);
            return matches;
        }
    }
}