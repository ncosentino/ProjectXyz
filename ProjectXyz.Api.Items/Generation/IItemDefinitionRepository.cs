using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemDefinitionRepository
    {
        IEnumerable<IItemDefinition> LoadItemDefinitions(IFilterContext filterContext);
    }
}