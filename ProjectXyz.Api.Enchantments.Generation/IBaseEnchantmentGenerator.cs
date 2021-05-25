using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IBaseEnchantmentGenerator
    {
        IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext);
    }
}