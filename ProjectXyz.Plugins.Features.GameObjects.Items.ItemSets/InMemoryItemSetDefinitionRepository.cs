using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class InMemoryItemSetDefinitionRepository : IDiscoverableItemSetDefinitionRepository
    {
        private readonly List<IItemSetDefinition> _itemSetDefinitions;

        public InMemoryItemSetDefinitionRepository(IEnumerable<IItemSetDefinition> itemSetDefinitions)
        {
            _itemSetDefinitions = new List<IItemSetDefinition>(itemSetDefinitions);
        }

        public IItemSetDefinition GetItemSetDefinitionById(IIdentifier itemSetDefinitionId)
        {
            if (!_itemSetDefinitions.Any())
            {
                throw new InvalidOperationException(
                    $"There are no {typeof(IItemSetDefinition)} instances " +
                    $"for this repository. Did you forget to construct " +
                    $"{GetType()} with these instances or register them with " +
                    $"your dependency injection framework?");
            }

            return _itemSetDefinitions.FirstOrDefault(x => x.Id.Equals(itemSetDefinitionId));
        }
    }
}
