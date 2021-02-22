using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentLoader
    {
        IEnumerable<IEnchantment> Load(IFilterContext filterContext);
    }
}