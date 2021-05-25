using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}