using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IFilterContextFactory
    {
        IFilterContext CreateContext(
            int minimumCount,
            int maximumCount,
            params IFilterAttribute[] attributes);

        IFilterContext CreateContext(
            int minimumCount,
            int maximumCount,
            IEnumerable<IFilterAttribute> attributes);

        IFilterContext CreateContext(
            IFilterContext source,
            params IFilterAttribute[] attributes);

        IFilterContext CreateContext(
            IFilterContext source,
            IEnumerable<IFilterAttribute> attributes);
    }
}