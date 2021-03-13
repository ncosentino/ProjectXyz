using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}