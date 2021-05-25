using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default
{
    public sealed class FilterContextProvider : IFilterContextProvider
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IReadOnlyCollection<IDiscoverableFilterContextAttributeProvider> _filterContextAttributeProviders;

        public FilterContextProvider(
            IFilterContextFactory filterContextFactory,
            IEnumerable<IDiscoverableFilterContextAttributeProvider> filterContextAttributeProviders)
        {
            _filterContextFactory = filterContextFactory;
            _filterContextAttributeProviders = filterContextAttributeProviders.ToArray();
        }

        public IFilterContext GetContext()
        {
            var attributes = _filterContextAttributeProviders.SelectMany(x => x.GetAttributes());
            var newContext = _filterContextFactory.CreateContext(
                0,
                int.MaxValue,
                attributes);
            return newContext;
        }
    }
}