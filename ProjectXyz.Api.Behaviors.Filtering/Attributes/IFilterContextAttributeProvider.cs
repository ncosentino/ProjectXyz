using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IFilterContextAttributeProvider
    {
        IEnumerable<IFilterAttribute> GetAttributes();
    }
}