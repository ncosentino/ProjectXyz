using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IBaseEnchantmentGenerator
    {
        IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext);
    }
}