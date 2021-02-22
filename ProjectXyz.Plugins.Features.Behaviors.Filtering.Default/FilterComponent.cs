using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class FilterComponent : IFilterComponent
    {
        public FilterComponent()
            : this(Enumerable.Empty<IFilterAttribute>())
        {
        }

        public FilterComponent(IEnumerable<IFilterAttribute> supportedAttributes)
        {
            SupportedAttributes = supportedAttributes.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }
    }
}