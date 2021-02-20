using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IFilterContext
    {
        int MinimumCount { get; }

        int MaximumCount { get; }

        IEnumerable<IFilterAttribute> Attributes { get; }
    }
}