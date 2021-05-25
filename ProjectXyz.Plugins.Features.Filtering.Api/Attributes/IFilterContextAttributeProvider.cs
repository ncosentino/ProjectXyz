using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IFilterContextAttributeProvider
    {
        IEnumerable<IFilterAttribute> GetAttributes();
    }
}