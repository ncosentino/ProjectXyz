using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}