using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IAttributeFilterer
    {
        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IReadOnlyCollection<IFilterAttributeValue> filterAttributes);

        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes;

        IEnumerable<T> BidirectionalFilter<T>(
            IEnumerable<T> source,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes;
    }
}