using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentGenerator : IHasGeneratorAttributes
    {
        IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext);
    }
}