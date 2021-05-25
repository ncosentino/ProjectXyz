using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default
{
    /// <summary>
    /// These extensions require access to the class 
    /// <see cref="FilterContext"/>. As a result, they cannot be defined in 
    /// the API library.
    /// </summary>
    public static class FilterContextExtensionMethods
    {
        public static IFilterContext WithAdditionalAttributes(
            this IFilterContext filterContext,
            IEnumerable<IFilterAttribute> additionalFilterAttributes)
        {
            var newContext = new FilterContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
                    .Attributes
                    .Concat(additionalFilterAttributes));
            return newContext;
        }

        public static IFilterContext WithRange(
            this IFilterContext filterContext,
            int minimumCount,
            int maximumCount)
        {
            var newContext = new FilterContext(
                minimumCount,
                maximumCount,
                filterContext.Attributes);
            return newContext;
        }
    }
}