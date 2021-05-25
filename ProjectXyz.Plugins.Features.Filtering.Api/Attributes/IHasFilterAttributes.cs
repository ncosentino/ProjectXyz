using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IHasFilterAttributes
    {
        IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}