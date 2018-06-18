using System.Collections.Generic;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemDefinition : IHasItemGeneratorAttributes
    {
        IEnumerable<IItemGeneratorComponent> GeneratorComponents { get; }
    }
}