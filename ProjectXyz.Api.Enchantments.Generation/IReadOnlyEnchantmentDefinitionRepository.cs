using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IReadOnlyEnchantmentDefinitionRepository
    {
        IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IGeneratorContext generatorContext);
    }
}