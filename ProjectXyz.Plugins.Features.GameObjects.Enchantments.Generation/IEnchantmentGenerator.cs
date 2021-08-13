using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation
{
    public interface IEnchantmentGenerator : IHasFilterAttributes
    {
        IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext);
    }
}