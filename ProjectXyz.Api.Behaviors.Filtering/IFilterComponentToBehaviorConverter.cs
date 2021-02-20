using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IFilterComponentToBehaviorConverter
    {
        IEnumerable<IBehavior> Convert(IFilterComponent filterComponent);
    }
}