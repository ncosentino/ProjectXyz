using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentDefinition : IHasGeneratorAttributes
    {
        IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}