using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class ItemDefinitionRepositoryFacade : IItemDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableItemDefinitionRepository> _repositories;

        public ItemDefinitionRepositoryFacade(IEnumerable<IDiscoverableItemDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IItemDefinition> LoadItemDefinitions(IGeneratorContext generatorContext) =>
            _repositories.SelectMany(x => x.LoadItemDefinitions(generatorContext));
    }
}