using System.Collections.Generic;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentDefinitionRepository : IReadOnlyEnchantmentDefinitionRepository
    {
        void WriteEnchantmentDefinitions(IEnumerable<IEnchantmentDefinition> enchantmentDefinitions);
    }
}