using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation
{
    public interface IReadOnlyEnchantmentDefinitionRepository
    {
        IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext);
    }
}