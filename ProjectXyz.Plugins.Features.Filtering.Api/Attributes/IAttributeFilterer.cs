using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IAttributeFilterer
    {
        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IFilterContext filterContext)
            where T : IHasFilterAttributes;
    }
}