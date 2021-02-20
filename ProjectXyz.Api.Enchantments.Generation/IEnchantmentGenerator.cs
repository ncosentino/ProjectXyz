using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentGenerator : IHasFilterAttributes
    {
        IEnumerable<IEnchantment> GenerateEnchantments(IFilterContext filterContext);
    }
}