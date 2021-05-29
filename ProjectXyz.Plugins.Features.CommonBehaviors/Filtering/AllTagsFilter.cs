using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class AllTagsFilter : IFilterAttributeValue
    {
        public AllTagsFilter(params IIdentifier[] tags)
            : this((IEnumerable<IIdentifier>)tags)
        {
        }

        public AllTagsFilter(IEnumerable<IIdentifier> tags)
        {
            Tags = tags.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> Tags { get; }
    }
}
