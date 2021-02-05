using System.Linq;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public static class IEnchantmentDefinitionRepositoryExtensions
    {
        public static void WriteEnchantmentDefinitions(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentDefinition enchantmentDefinition)
        {
            enchantmentDefinitionRepository.WriteEnchantmentDefinitions(new[] { enchantmentDefinition });
        }

        public static void WriteEnchantmentDefinitions(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentDefinition enchantmentDefinition,
            params IEnchantmentDefinition[] otherEnchantmentDefinitions)
        {
            enchantmentDefinitionRepository.WriteEnchantmentDefinitions(
                new[] { enchantmentDefinition }.Concat(otherEnchantmentDefinitions));
        }
    }
}