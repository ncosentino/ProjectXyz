using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class ItemDefinitionRepositoryFacade : IItemDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableItemDefinitionRepository> _repositories;

        public ItemDefinitionRepositoryFacade(IEnumerable<IDiscoverableItemDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IItemDefinition> LoadItemDefinitions(IFilterContext filterContext) =>
            _repositories.SelectMany(x => x.LoadItemDefinitions(filterContext));
    }
}