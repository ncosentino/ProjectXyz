using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation
{
    public interface IEnchantmentDefinitionRepository : IReadOnlyEnchantmentDefinitionRepository
    {
        void WriteEnchantmentDefinitions(IEnumerable<IEnchantmentDefinition> enchantmentDefinitions);
    }
}