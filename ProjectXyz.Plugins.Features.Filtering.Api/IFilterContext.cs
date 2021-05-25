using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Api
{
    public interface IFilterContext
    {
        int MinimumCount { get; }

        int MaximumCount { get; }

        IEnumerable<IFilterAttribute> Attributes { get; }
    }
}