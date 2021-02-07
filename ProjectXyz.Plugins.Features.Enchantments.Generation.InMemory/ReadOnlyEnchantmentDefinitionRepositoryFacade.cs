using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory
{
    public sealed class ReadOnlyEnchantmentDefinitionRepositoryFacade : IReadOnlyEnchantmentDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableReadOnlyEnchantmentDefinitionRepository> _repositories;

        public ReadOnlyEnchantmentDefinitionRepositoryFacade(IEnumerable<IDiscoverableReadOnlyEnchantmentDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IGeneratorContext generatorContext) =>
            _repositories.SelectMany(x => x.ReadEnchantmentDefinitions(generatorContext));
    }
}