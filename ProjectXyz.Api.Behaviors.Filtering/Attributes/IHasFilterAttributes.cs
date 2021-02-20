using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IHasFilterAttributes
    {
        IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}