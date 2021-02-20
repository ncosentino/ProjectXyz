using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IAttributeFilterer
    {
        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IFilterContext filterContext)
            where T : IHasFilterAttributes;
    }
}