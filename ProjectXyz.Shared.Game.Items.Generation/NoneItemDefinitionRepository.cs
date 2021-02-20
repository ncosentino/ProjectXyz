using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class NoneItemDefinitionRepository : IDiscoverableItemDefinitionRepository
    {
        public IEnumerable<IItemDefinition> LoadItemDefinitions(IFilterContext filterContext) =>
            Enumerable.Empty<IItemDefinition>();
    }
}