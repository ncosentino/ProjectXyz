using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments.Generation;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory
{
    public sealed class ReadOnlyEnchantmentDefinitionRepositoryFacade : IReadOnlyEnchantmentDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableReadOnlyEnchantmentDefinitionRepository> _repositories;

        public ReadOnlyEnchantmentDefinitionRepositoryFacade(IEnumerable<IDiscoverableReadOnlyEnchantmentDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext) =>
            _repositories.SelectMany(x => x.ReadEnchantmentDefinitions(filterContext));
    }
}