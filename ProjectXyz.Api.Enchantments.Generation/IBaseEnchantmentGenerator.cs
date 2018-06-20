using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IBaseEnchantmentGenerator
    {
        IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext);
    }
}