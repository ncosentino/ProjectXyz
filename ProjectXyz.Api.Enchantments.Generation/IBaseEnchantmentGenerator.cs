using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IBaseEnchantmentGenerator
    {
        IEnumerable<IEnchantment> GenerateEnchantments(IFilterContext filterContext);
    }
}