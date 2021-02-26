using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetDefinitionRepositoryFacade : IItemSetDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableItemSetDefinitionRepository> _repositories;

        public ItemSetDefinitionRepositoryFacade(IEnumerable<IDiscoverableItemSetDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IItemSetDefinition GetItemSetDefinitionById(IIdentifier itemSetDefinitionId) => _repositories
            .Select(x => x.GetItemSetDefinitionById(itemSetDefinitionId))
            .FirstOrDefault(x => x != null);
    }
}
