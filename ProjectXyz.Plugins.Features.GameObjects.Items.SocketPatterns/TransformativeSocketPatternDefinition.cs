using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class TransformativeSocketPatternDefinition : ITransformativeSocketPatternDefinition
    {
        public TransformativeSocketPatternDefinition(
            IEnumerable<IFilterAttributeValue> filters,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            Filters = filters.ToArray();
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IReadOnlyCollection<IFilterAttributeValue> Filters { get; }

        public IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}
