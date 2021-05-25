using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IReadOnlyEnchantmentDefinitionRepository
    {
        IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext);
    }
}