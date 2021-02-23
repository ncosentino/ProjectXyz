using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class FilterContextFactory : IFilterContextFactory
    {
        public IFilterContext CreateContext(
            IFilterContext source,
            params IFilterAttribute[] attributes)
        {
            var newContext = CreateContext(
                source,
                (IEnumerable<IFilterAttribute>)attributes);
            return newContext;
        }

        public IFilterContext CreateContext(
            IFilterContext source,
            IEnumerable<IFilterAttribute> attributes)
        {
            //
            // TODO: do we need to do fancy intelligent merging?
            //
            var mergedAttributes = source
                .Attributes
                .Concat(attributes);

            var newContext = CreateContext(
                source.MinimumCount,
                source.MaximumCount,
                mergedAttributes);
            return newContext;
        }

        public IFilterContext CreateContext(
            int minimumCount,
            int maximumCount,
            params IFilterAttribute[] attributes)
        {
            var newContext = CreateContext(
                minimumCount,
                maximumCount,
                (IEnumerable<IFilterAttribute>)attributes);
            return newContext;
        }

        public IFilterContext CreateContext(
            int minimumCount,
            int maximumCount,
            IEnumerable<IFilterAttribute> attributes)
        {
            var itemFilterContext = new FilterContext(
                minimumCount,
                maximumCount,
                attributes);
            return itemFilterContext;
        }
    }

    public interface IBehaviorFilterComponent : IFilterComponent
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}