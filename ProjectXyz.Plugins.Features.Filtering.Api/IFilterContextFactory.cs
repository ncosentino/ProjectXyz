using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Api
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