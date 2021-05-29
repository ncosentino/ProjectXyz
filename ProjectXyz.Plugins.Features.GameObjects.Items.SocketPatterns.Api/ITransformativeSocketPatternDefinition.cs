using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ITransformativeSocketPatternDefinition
    {
        IReadOnlyCollection<IFilterAttributeValue> Filters { get; }

        IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}
