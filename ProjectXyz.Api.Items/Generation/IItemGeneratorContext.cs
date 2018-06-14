using System.Collections.Generic;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGeneratorContext
    {
        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IItemGeneratorAttribute> Attributes { get; }
    }
}