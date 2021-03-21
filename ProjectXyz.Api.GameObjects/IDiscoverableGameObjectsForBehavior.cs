using System;

namespace ProjectXyz.Api.GameObjects
{
    public interface IDiscoverableGameObjectsForBehavior : IGameObjectsForBehavior
    {
        Type SupportedBehaviorType { get; }
    }
}