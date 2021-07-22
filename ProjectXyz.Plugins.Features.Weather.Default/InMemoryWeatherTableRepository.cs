using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Weather.Default
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
            var matches = _attributeFilterer.BidirectionalFilter(
                _weatherTables,
                filterContext.Attributes);
            return matches;
        }
    }
}