using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public delegate IEnumerable<IBehavior> ConvertFilterComponentToBehaviorDelegate(IFilterComponent filterComponent);
}