using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class AnyTagsFilter : IFilterAttributeValue
    {
        public AnyTagsFilter(params IIdentifier[] tags)
            : this((IEnumerable<IIdentifier>)tags)
        {
        }

        public AnyTagsFilter(IEnumerable<IIdentifier> tags)
        {
            Tags = tags.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> Tags { get; }
    }
}
