using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class BehaviorFilterComponent : IBehaviorFilterComponent
    {
        public BehaviorFilterComponent(
            IEnumerable<IFilterAttribute> supportedAttributes,
            params IBehavior[] behaviors)
            : this(supportedAttributes, (IEnumerable<IBehavior>)behaviors)
        {
        }

        public BehaviorFilterComponent(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IBehavior> behaviors)
        {
            SupportedAttributes = supportedAttributes;
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}