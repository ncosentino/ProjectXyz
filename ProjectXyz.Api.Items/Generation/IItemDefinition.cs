using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.GameObjects.Items.Generation
{
    public interface IItemDefinition : IHasGeneratorAttributes
    {
        IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}