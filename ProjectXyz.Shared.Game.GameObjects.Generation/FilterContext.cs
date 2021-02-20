using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Shared.Behaviors.Filtering
{
    public sealed class FilterContext : IFilterContext
    {
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