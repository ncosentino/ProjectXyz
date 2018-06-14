using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation
{
    public sealed class ItemGeneratorContext : IItemGeneratorContext
    {
        public ItemGeneratorContext(
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IItemGeneratorAttribute> attributes)
        {
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Attributes = attributes.ToArray();
        }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IItemGeneratorAttribute> Attributes { get; }
    }
}