using System;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IDiscoverableGeneratorComponentToBehaviorConverter : IGeneratorComponentToBehaviorConverter
    {
        Type ComponentType { get; }
    }
}