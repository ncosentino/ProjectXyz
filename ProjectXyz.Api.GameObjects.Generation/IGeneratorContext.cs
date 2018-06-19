using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorContext
    {
        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IGeneratorAttribute> Attributes { get; }
    }
}