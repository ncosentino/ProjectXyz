using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentDefinitionRepository
    {
        IEnumerable<IEnchantmentDefinition> LoadEnchantmentDefinitions(IGeneratorContext generatorContext);
    }
}