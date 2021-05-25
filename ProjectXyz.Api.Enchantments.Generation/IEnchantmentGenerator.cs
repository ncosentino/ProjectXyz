using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentGenerator : IHasFilterAttributes
    {
        IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext);
    }
}