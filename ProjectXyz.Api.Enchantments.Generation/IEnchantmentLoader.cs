using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentLoader
    {
        IEnumerable<IEnchantment> Load(IFilterContext filterContext);

        IEnumerable<IEnchantment> LoadForEnchantmenDefinitionIds(IEnumerable<IIdentifier> enchantmentDefinitionIds);
    }
}