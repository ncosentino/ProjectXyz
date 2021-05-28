using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default
{
    public sealed class FilterContext : IFilterContext
    {
        public FilterContext(IFilterAttribute attribute)
            : this(new[] { attribute })
        {
        }

        public FilterContext(IEnumerable<IFilterAttribute> attributes)
            : this(0, int.MaxValue, attributes)
        {
        }

        public FilterContext(
            int minimumCount,
            int maximumCount,
            IFilterAttribute attribute)
            : this(minimumCount, maximumCount, new[] { attribute })
        {
        }

        public FilterContext(
            int minimumCount,
            int maximumCount,
            IEnumerable<IFilterAttribute> attributes)
        {
            MinimumCount = minimumCount;
            MaximumCount = maximumCount;
            Attributes = attributes.ToArray();
        }

        public int MinimumCount { get; }

        public int MaximumCount { get; }

        public IEnumerable<IFilterAttribute> Attributes { get; }
    }
}