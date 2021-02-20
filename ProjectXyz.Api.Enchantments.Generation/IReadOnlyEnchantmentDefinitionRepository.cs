using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IReadOnlyEnchantmentDefinitionRepository
    {
        IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext);
    }
}