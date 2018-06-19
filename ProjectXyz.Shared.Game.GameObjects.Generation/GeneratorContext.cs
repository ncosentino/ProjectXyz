using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
{
    public sealed class GeneratorContext : IGeneratorContext
    {
        public GeneratorContext(
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IGeneratorAttribute> attributes)
        {
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Attributes = attributes.ToArray();
        }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IGeneratorAttribute> Attributes { get; }
    }
}