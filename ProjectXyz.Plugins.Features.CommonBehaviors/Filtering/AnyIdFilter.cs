using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class AnyIdFilter : IFilterAttributeValue
    {
        public AnyIdFilter(params IIdentifier[] ids)
            : this((IEnumerable<IIdentifier>)ids)
        {
        }

        public AnyIdFilter(IEnumerable<IIdentifier> ids)
        {
            Ids = ids.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> Ids { get; }
    }
}
