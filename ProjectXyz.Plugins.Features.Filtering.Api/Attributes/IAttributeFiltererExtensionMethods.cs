using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public static class IAttributeFiltererExtensionMethods
    {
        public static bool IsMatch<T>(
            this IAttributeFilterer attributeFilterer,
            T single,
            IReadOnlyCollection<IFilterAttributeValue> filterAttributes)
        {
            var match = attributeFilterer
                .Filter(
                    new[] { single },
                    filterAttributes)
                .Any();
            return match;
        }

        public static bool IsMatch<T>(
            this IAttributeFilterer attributeFilterer,
            T single,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes
        {
            var match = attributeFilterer
                .Filter(
                    new[] { single },
                    filterAttributes)
                .Any();
            return match;
        }

        public static bool IsBidirectionalMatch<T>(
            this IAttributeFilterer attributeFilterer,
            T single,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes
        {
            var match = attributeFilterer
                .BidirectionalFilter(
                    new[] { single },
                    filterAttributes)
                .Any();
            return match;
        }
    }
}