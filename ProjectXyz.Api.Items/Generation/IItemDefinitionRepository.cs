using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemDefinitionRepository
    {
        IEnumerable<IItemDefinition> LoadItemDefinitions(IFilterContext filterContext);
    }
}