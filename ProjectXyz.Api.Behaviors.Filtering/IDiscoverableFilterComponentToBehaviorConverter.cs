using System;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IDiscoverableFilterComponentToBehaviorConverter : IFilterComponentToBehaviorConverter
    {
        Type ComponentType { get; }
    }
}