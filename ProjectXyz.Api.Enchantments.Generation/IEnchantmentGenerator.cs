using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentGenerator : IHasFilterAttributes
    {
        IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext);
    }
}