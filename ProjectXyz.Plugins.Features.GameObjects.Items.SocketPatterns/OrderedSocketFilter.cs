using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class OrderedSocketFilter : IFilterAttributeValue
    {
        public OrderedSocketFilter(IEnumerable<IReadOnlyCollection<IFilterAttributeValue>> perSocketFilters)
        {
            PerSocketFilters = perSocketFilters.ToArray();
        }

        public IReadOnlyCollection<IReadOnlyCollection<IFilterAttributeValue>> PerSocketFilters { get; }
    }
}
