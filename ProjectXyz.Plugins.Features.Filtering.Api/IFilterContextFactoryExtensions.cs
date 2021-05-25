using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Api
{
    public static class IFilterContextFactoryExtensions
    {
        public static IFilterContext CreateFilterContextForSingle(
            this IFilterContextFactory filterContextFactory,
            params IFilterAttribute[] attributes)
        {
            var filterContext = filterContextFactory.CreateContext(
                1,
                1,
                attributes);
            return filterContext;
        }

        public static IFilterContext CreateFilterContextForSingle(
            this IFilterContextFactory filterContextFactory,
            IEnumerable<IFilterAttribute> attributes)
        {
            var filterContext = filterContextFactory.CreateContext(
                1,
                1,
                attributes);
            return filterContext;
        }

        public static IFilterContext CreateFilterContextForAnyAmount(
            this IFilterContextFactory filterContextFactory,
            params IFilterAttribute[] attributes)
        {
            var filterContext = filterContextFactory.CreateContext(
                0,
                int.MaxValue,
                attributes);
            return filterContext;
        }

        public static IFilterContext CreateFilterContextForAnyAmount(
            this IFilterContextFactory filterContextFactory,
            IEnumerable<IFilterAttribute> attributes)
        {
            var filterContext = filterContextFactory.CreateContext(
                0,
                int.MaxValue,
                attributes);
            return filterContext;
        }

        public static IFilterContext CreateNoneFilterContext(this IFilterContextFactory filterContextFactory)
        {
            var filterContext = filterContextFactory.CreateContext(
                0,
                0,
                Enumerable.Empty<IFilterAttribute>());
            return filterContext;
        }
    }
}