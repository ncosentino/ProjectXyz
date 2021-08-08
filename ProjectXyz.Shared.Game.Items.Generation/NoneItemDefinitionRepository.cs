using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class NoneItemDefinitionRepository : IDiscoverableItemDefinitionRepository
    {
        public IEnumerable<IItemDefinition> LoadItemDefinitions(IFilterContext filterContext) =>
            Enumerable.Empty<IItemDefinition>();
    }
}